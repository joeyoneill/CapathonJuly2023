using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CAPATHON.Controllers
{
    public class SessionController : Controller
    {
        private readonly HHDBContext _context;

        public SessionController(HHDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        ////////////////////////////////////////////////////////////////////////////////
        // Precluded Sign Up Decision Forms
        ////////////////////////////////////////////////////////////////////////////////

        // GET: /Session/SelectCareType
        [Authorize]
        public IActionResult SelectCareType() {
            // null check
            if (_context.CareTypes == null)
                return NotFound();
            
            // Get Care Types + Add to ViewBag
            var careTypes = _context.CareTypes.ToList();
            if (careTypes == null)
                return NotFound();
            ViewBag.CareTypes = new SelectList(careTypes, "Id", "Name");

            return View();
        }

        // POST: /Session/SelectCareType
        [HttpPost]
        [Authorize]
        public IActionResult SelectCareType(int careTypeId) {
            Console.WriteLine("\ncaretype id: " + careTypeId + "\n");
            return RedirectToAction("Index");
        }

        ////////////////////////////////////////////////////////////////////////////////// Care Sign Up Form
        ////////////////////////////////////////////////////////////////////////////////

        // GET: /Session/CareSessionForm
        [Authorize]
        public IActionResult CareSessionForm() {
            return View();
        }
    }
}