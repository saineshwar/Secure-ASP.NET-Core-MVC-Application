using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreSecurity.EntityStore;
using CoreSecurity.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace CoreSecurity.Controllers
{
    /// <summary>
    /// 2. Solution Cross-Site Request Forgery (CSRF)
    /// </summary>
    public class RegistrationController : Controller
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IHostingEnvironment _environment;
        public RegistrationController(DatabaseContext databaseContext, IHostingEnvironment hostingEnvironment)
        {
            _databaseContext = databaseContext;
            _environment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Registration registration)
        {
            if (ModelState.IsValid)
            {
                registration.Status = true;
                registration.CreatedDate = DateTime.Now;
                _databaseContext.Registration.Add(registration);
                _databaseContext.SaveChanges();
                return RedirectToAction("Register", "Registration");
            }
            return View();

        }


    }
}