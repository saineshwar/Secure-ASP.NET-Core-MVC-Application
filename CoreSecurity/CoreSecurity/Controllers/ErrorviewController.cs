using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CoreSecurity.Controllers
{
    public class ErrorviewController : Controller
    {
        public IActionResult Error()
        {
            return View();
        }
    }
}