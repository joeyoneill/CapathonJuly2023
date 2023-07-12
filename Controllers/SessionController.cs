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

        ////////////////////////////////////////////////////////////////////////////////// Care Sign Up Form
        ////////////////////////////////////////////////////////////////////////////////

        // GET: /Session/CareSessionForm
        [Authorize]
        public async Task<IActionResult> CareSessionForm() {

            return View();
        }
    }
}