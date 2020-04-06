using System;
using System.Net.WebSockets;

namespace CustomERP.Web.Areas.Production.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using CustomERP.Common;
    using CustomERP.Data;
    using CustomERP.Data.Models;
    using CustomERP.Services.Data;
    using CustomERP.Web.Controllers;
    using CustomERP.Web.ViewModels.Production.Sections;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Authorize(Roles = GlobalConstants.RoleName.Administrator)]
    [Area("Production")]
    public class SectionsController : BaseController
    {
        private readonly ApplicationDbContext context;
        private readonly IDepartmentsService departmentsService;
        private readonly ISectionsService sectionsService;
        private readonly UserManager<ApplicationUser> userManager;

        public SectionsController(ApplicationDbContext context, IDepartmentsService departmentsService, ISectionsService sectionsService, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.departmentsService = departmentsService;
            this.sectionsService = sectionsService;
            this.userManager = userManager;
        }

        // GET: Production/Sections
        public IActionResult Index()
        {
            var sections = this.sectionsService.GetAll<SectionIndexViewModel>();
            var viewModel = new SectionsListViewModel { Sections = sections }.Sections;

            return this.View(viewModel);
        }

        // GET: Production/Sections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var section = await this.context.Sections
                .Include(s => s.Order)
                .Include(s => s.SectionParent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (section == null)
            {
                return this.NotFound();
            }

            return this.View(section);
        }

        // GET: Production/Sections/Create
        [Authorize]
        public IActionResult Create()
        {
            var departments = this.departmentsService.GetAll<DepartmentDropDownViewModel>()
                .Prepend(new DepartmentDropDownViewModel
                {
                    Name = string.Empty,
                    Id = 0,
                });
            var sections = this.sectionsService.GetAll<SectionDropDownViewModel>()
                .Prepend(new SectionDropDownViewModel
                {
                    Name = string.Empty,
                    Id = 0,
                });

            var viewModel = new SectionCreateInputModel
            {
                Departments = departments,
                Sections = sections,
            };

            return this.View(viewModel);
        }

        // POST: Production/Sections/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SectionCreateInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var sectionParentId =
                input.SectionParentId == 0
                ? (int?)null
                : input.SectionParentId;

            var sectionId = await this.sectionsService.CreateAsync(input.Name, input.DepartmentId, sectionParentId, user.Id);

            return this.RedirectToAction(nameof(this.Details), new { id = sectionId });
        }

        // GET: Production/Sections/Edit/5
        public IActionResult Edit(int id)
        {
            var sectionViewModel = this.sectionsService.GetById<SectionEditInputModel>(id);
            var departments = this.departmentsService.GetAll<DepartmentDropDownViewModel>()
                .Prepend(new DepartmentDropDownViewModel
                {
                    Name = string.Empty,
                    Id = 0,
                });
            var sections = this.sectionsService.GetAll<SectionDropDownViewModel>()
                .Prepend(new SectionDropDownViewModel
                {
                    Name = string.Empty,
                    Id = 0,
                });
            sectionViewModel.Sections = sections;
            sectionViewModel.Departments = departments;

            if (sectionViewModel == null)
            {
                return this.NotFound();
            }

            this.TempData["Id"] = id;
            return this.View(sectionViewModel);
        }

        // POST: Production/Sections/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SectionEditInputModel model)//[Bind("Name,OrderId,SectionParentId,IsDeleted,DeletedOn,DeletedFrom,Id,CreatedOn,CreatorUserId,ModifiedOn,ModifierUserId")] Section section)
        {
            //if (id != section.Id)
            //{
            //    return this.NotFound();
            //}
            var section = new Section();
            var test = this.TempData["Id"];
            if (this.ModelState.IsValid)
            {
                try
                {
                    this.context.Update(section);
                    await this.context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.SectionExists(section.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["OrderId"] = new SelectList(this.context.Orders, "Id", "Recipe", section.OrderId);
            this.ViewData["SectionParentId"] = new SelectList(this.context.Sections, "Id", "Name", section.SectionParentId);
            return this.View();
        }

        // GET: Production/Sections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var section = await this.context.Sections
                .Include(s => s.Order)
                .Include(s => s.SectionParent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (section == null)
            {
                return this.NotFound();
            }

            return this.View(section);
        }

        // POST: Production/Sections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var section = await this.context.Sections.FindAsync(id);
            this.context.Sections.Remove(section);
            await this.context.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool SectionExists(int id)
        {
            return this.context.Sections.Any(e => e.Id == id);
        }
    }
}
