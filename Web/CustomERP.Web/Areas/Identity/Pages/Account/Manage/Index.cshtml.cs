namespace CustomERP.Web.Areas.Identity.Pages.Account.Manage
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    using CustomERP.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        //[BindProperty]
        //public InputModel Usernam { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await this._userManager.GetUserNameAsync(user);
            var phoneNumber = await this._userManager.GetPhoneNumberAsync(user);

            this.Username = userName;

            this.Input = new InputModel
            {
                Username = userName,
                PhoneNumber = phoneNumber,
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await this._userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this._userManager.GetUserId(this.User)}'.");
            }

            await this.LoadAsync(user);
            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userTest = this.Input.Username;
            var user = await this._userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this._userManager.GetUserId(this.User)}'.");
            }

            if (!this.ModelState.IsValid)
            {
                await this.LoadAsync(user);
                return this.Page();
            }

            var phoneNumber = await this._userManager.GetPhoneNumberAsync(user);
            if (this.Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await this._userManager.SetPhoneNumberAsync(user, this.Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await this._userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }

            await this._signInManager.RefreshSignInAsync(user);
            this.StatusMessage = "Your profile has been updated";
            return this.RedirectToPage();
        }
    }
}
