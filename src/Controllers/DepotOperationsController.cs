using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CarShowRoom.Controllers
{
    public class DepotOperationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}