using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("/[controller]/[action]")]
    public class RouterTController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
    }
}