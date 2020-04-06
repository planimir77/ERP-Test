namespace CustomERP.Web.ViewModels.Administration.Register
{
    using System.ComponentModel.DataAnnotations;

    public class UserInputViewModel
    {
        [MinLength(8)]
        [Required]
        [Display(Name = "Full Name")]
        [RegularExpression(
            "^([A-Z][a-z]+\\s[A-Z][a-z]+\\s[A-Z][a-z]+|[А-Я][а-я]+\\s[А-Я][а-я]+\\s[А-Я][а-я]+)")]
        public string FullName { get; set; }

        [Required]
        [Range(3, 20)]
        public string Position { get; set; }
    }
}
