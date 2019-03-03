using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano.Data;
using Nano.Web.Models;

namespace Nano.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly NanoDbContext _dbContext;

        public HomeController(NanoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            _dbContext.Set<TT>().Add(new TT
            {
                Name = "Ahmad"
            });
            _dbContext.SaveChanges();
            Console.WriteLine(" ---------------------- " + _dbContext.Set<TT>().FirstOrDefault()?.Name);
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
