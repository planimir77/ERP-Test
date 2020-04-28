using CustomERP.Common;
using CustomERP.Data.Common.Repositories;
using CustomERP.Services.Mapping;
using CustomERP.Web.ViewModels.Production.Schedules;

namespace CustomERP.Web.Areas.Production.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using CustomERP.Data;
    using CustomERP.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Area("Production")]
    public class SchedulesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDeletableEntityRepository<Schedule> scheduleRepository;

        public SchedulesController(ApplicationDbContext context, IDeletableEntityRepository<Schedule> scheduleRepository)
        {
            _context = context;
            this.scheduleRepository = scheduleRepository;
        }

        // GET: Production/Schedules
        public IActionResult Index()
        {
            var schedules = this.scheduleRepository.All().To<SchedulesViewModel>();

            var viewModel = new SchedulesListViewModel
            {
                SchedulesViewModels = schedules,
            };

            return this.View(viewModel);
        }

        // GET: Production/Schedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // GET: Production/Schedules/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Production/Schedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ScheduleCreateViewModel inputModel)
        {
            var schedule = this.scheduleRepository.All().FirstOrDefault(x => x.Name == inputModel.Name);

            if (schedule != null)
            {
                this.TempData["Message"] = GlobalConstants.Messages.AlreadyExist;
                return View(inputModel);
            }

            if (this.ModelState.IsValid)
            {
                schedule = new Schedule
                {
                    Name = inputModel.Name,
                    NumberOfDays = inputModel.NumberOfDays,
                    CreatedFrom = "Not Implemented User",
                };
                await this.scheduleRepository.AddAsync(schedule);
                await this.scheduleRepository.SaveChangesAsync();

                return this.RedirectToAction("Index");
            }

            return View(inputModel);
        }

        // GET: Production/Schedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }
            return View(schedule);
        }

        // POST: Production/Schedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,NumberOfDays,IsDeleted,DeletedOn,DeletedFrom,Id,CreatedOn,ModifiedOn,CreatedFrom,ModifiedFrom")] Schedule schedule)
        {
            if (id != schedule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleExists(schedule.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(schedule);
        }

        // GET: Production/Schedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // POST: Production/Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduleExists(int id)
        {
            return _context.Schedules.Any(e => e.Id == id);
        }
    }
}
