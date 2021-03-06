﻿namespace CustomERP.Web.Controllers
{
    using System.Diagnostics;

    using CustomERP.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            var generator = new GenerateViewModel();
            this.TempData["width"] = 10;
            return this.View(generator);
        }

        public IActionResult Test()
        {

            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
