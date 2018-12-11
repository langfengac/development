using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EFDemo.Models;
using Microsoft.Extensions.Caching.Memory;

namespace EFDemo.Controllers
{
    public class HomeController : Controller
    {
        private IMemoryCache _cache;

        public HomeController(IMemoryCache cache)
        {
            this._cache = cache;
        }
        public IActionResult Index()
        {
            var data="";
            if (!_cache.TryGetValue("name",out data))
            {
                data = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(5));

                _cache.Set<string>("name", data, cacheEntryOptions);
            }
           
            ViewBag.git = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            return View("Index",data);
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

        

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
