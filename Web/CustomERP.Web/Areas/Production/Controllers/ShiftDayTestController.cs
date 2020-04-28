namespace CustomERP.Web.Areas.Production.Controllers
{
    using CustomERP.Data;
    using Microsoft.AspNetCore.Mvc;

    [Area("Production")]
    public class ShiftDayTestController : Controller
    {
        private readonly ApplicationDbContext context;

        public ShiftDayTestController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Create(int id)
        {
            return this.View();
        }
    }
}
