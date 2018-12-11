using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EFDemo.Controllers
{
    [Route("test/[controller]/")]
    public class RouterTController : Controller
    {
        [Route("T1")]
        public IActionResult Index()
        {
            return View();
        }
    }
}