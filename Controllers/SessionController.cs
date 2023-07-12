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
        // SelectBusiness Functions
        ////////////////////////////////////////////////////////////////////////////////

        // GET: /Session/SelectBusiness
        [Authorize]
        public IActionResult SelectBusiness() {
            // table null check
            if (_context.Businesses == null)
                return NotFound();
            
            // Get Businesses + Add to ViewBag
            var businesses = _context.Businesses.ToList();
            if (businesses == null)
                return NotFound();
            ViewBag.Businesses = new SelectList(businesses, "Id", "Name");

            return View();
        }

        // POST: /Session/SelectBusiness
        [HttpPost]
        [Authorize]
        public IActionResult SelectBusiness(int businessId) {
            if (businessId == 0)
                return RedirectToAction("SelectBusiness");

            Console.WriteLine("\nbusiness id: " + businessId + "\n");
            return RedirectToAction("Index");
        }

        ////////////////////////////////////////////////////////////////////////////////
        // SelectCareType Functions
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