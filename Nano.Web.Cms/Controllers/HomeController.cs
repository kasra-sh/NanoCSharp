using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Nano.Core.Extensions;
using Nano.Core.IoC;
using Nano.Web.Cms.Models;
using Nano.Web.Cms.Services;

namespace Nano.Web.Cms.Controllers
{
    public class HomeController : Controller
    {
        private readonly ATestService _testService;
        private readonly ATestService2 _testService2;

        public HomeController(ATestService testService, ATestService2 testService2)
        {
            NanoEngineContext.Current.Resolve<ATestService>();
            _testService = testService;
            _testService2 = testService2;
            testService.SetId(Guid.NewGuid().ToString());
        }
        public IActionResult Index()
        {
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

        public IActionResult Test()
        {
//            var ts = NanoEngineContext.Current.GetService<ATestService>();
            return Ok(_testService.GetId()+" : "+_testService2.GetId());
        }
    }
}
