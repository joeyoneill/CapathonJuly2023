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

            return RedirectToAction("SelectLocation", new { businessId = businessId });
        }

        ////////////////////////////////////////////////////////////////////////////////
        // SelectLocation Functions
        ////////////////////////////////////////////////////////////////////////////////

        // GET: /Session/SelectLocation
        [Authorize]
        public async Task<IActionResult> SelectLocation(int? businessId) {
            if (_context.Locations == null || _context.Businesses == null)
                return NotFound();
            if (businessId == null)
                return RedirectToAction("SelectBusiness");

            // Get Business
            var business = await _context.Businesses.FindAsync(businessId);
            if (business == null)
                return NotFound();
            ViewBag.Business = business;
            
            // Get All Locations Where BusinessId == businessId
            var locations = _context.Locations.Where(l => l.BusinessId == businessId).ToList();
            if (locations == null)
                return NotFound();
            
            ViewBag.Locations = new SelectList(locations, "Id", "FullAddress");

            //
            return View();
        }

        // POST: /Session/SelectCareType
        [HttpPost]
        [Authorize]
        public IActionResult SelectLocationPost(int? locationId) {
            if (locationId == 0)
                return RedirectToAction("SelectBusiness");
            return RedirectToAction("SelectCareType", new { locationId = locationId });
        }

        ////////////////////////////////////////////////////////////////////////////////
        // SelectCareType Functions
        ////////////////////////////////////////////////////////////////////////////////

        // GET: /Session/SelectCareType
        [Authorize]
        public IActionResult SelectCareType(int? locationId) {

            // null check
            if (_context.CareTypes == null || locationId == null)
                return NotFound();
            
            // Get Care Types + Add to ViewBag
            var careTypes = _context.CareTypes.ToList();
            if (careTypes == null)
                return NotFound();
            ViewBag.CareTypes = new SelectList(careTypes, "Id", "Name");
            ViewBag.locationId = locationId;

            return View();
        }

        // POST: /Session/SelectCareTypePost
        [HttpPost]
        [Authorize]
        public IActionResult SelectCareTypePost(int careTypeId, int locationId) {
            return RedirectToAction("CareSessionForm", new { locationId, careTypeId });
        }

        ////////////////////////////////////////////////////////////////////////////////// Care Sign Up Form
        ////////////////////////////////////////////////////////////////////////////////

        // GET: /Session/CareSessionForm
        [Authorize]
        public IActionResult CareSessionForm(int locationId, int careTypeId) {
            if (_context.Sessions == null || _context.Dependents == null)
                return NotFound();
            
            // get sessions
            var sessions = _context.Sessions.Where(s => s.CareTypeId == careTypeId && s.LocationId == locationId).ToList();
            if (sessions == null)
                return NotFound();
            ViewBag.Sessions = new SelectList(sessions, "Id", "SessionTimeString");

            // Get user ID
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return NotFound();

            // Get user's dependents
            var dependents = _context.Dependents.Where(d => d.ClientId == userId);
            if (dependents == null)
                return NotFound();
            ViewBag.Dependents = new SelectList(dependents, "Id", "FullName");

            return View();
        }

        // POST: /Session/CareSessionFormPost
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CareSessionFormPost(Guid sessionId, Guid dependentId) {
            if (_context.Sessions == null || _context.Dependents == null || _context.SessionDependents == null)
                return NotFound();
            
            var sessionDependent = new SessionDependent {
                SessionId = sessionId,
                DependentId = dependentId
            };

            _context.SessionDependents.Add(sessionDependent);
            await _context.SaveChangesAsync();

            return RedirectToAction("Profile", "Account");
        }

        ////////////////////////////////////////////////////////////////////////////////// View Sessions Functions
        ////////////////////////////////////////////////////////////////////////////////

        [Authorize]
        public IActionResult ViewSessions() {
            // tables null check
            if (_context.SessionDependents == null || _context.Dependents == null || _context.Sessions == null || _context.Locations == null)
                return NotFound();

            // get client id
            var clientId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (clientId == null)
                return NotFound();
            
            // Get JOINed table information:
            // SessionDependents.Id
            // Dependents.FullName
            // Session.SessionTimeString
            // Session.LocationId
            // Location.FullAddress
            var sessionsInformation = _context.SessionDependents
                .Join(_context.Dependents,
                    sessionDependent => sessionDependent.DependentId,
                    dependent => dependent.Id,
                    (sessionDependent, dependent) => new { sessionDependent, dependent })
                .Join(_context.Sessions,
                    sdDependent => sdDependent.sessionDependent.SessionId,
                    session => session.Id,
                    (sdDependent, session) => new { sdDependent.sessionDependent, sdDependent.dependent, session })
                .Join(_context.Locations,
                    sdDependentSession => sdDependentSession.session.LocationId,
                    location => location.Id,
                    (sdDependentSession, location) => new
                    {
                        sdDependentSession.sessionDependent.Id,
                        sdDependentSession.dependent.FullName,
                        sdDependentSession.session.SessionTimeString,
                        LocationId = sdDependentSession.session.LocationId,
                        LocationFullAddress = location.FullAddress
                    })
                .ToList();
            
            ViewBag.SessionsInformation = sessionsInformation;

            return View();
        }

        // DELETE SessionDependent
    }
}