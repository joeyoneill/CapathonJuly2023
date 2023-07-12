using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CAPATHON.Controllers
{
    public class AccountController : Controller
    {
        private readonly HHDBContext _context;

        public AccountController(HHDBContext context)
        {
            _context = context;
        }

        ////////////////////////////////////////////////////////////////////////////////

        // ACTION: /Account/Login
        public async Task Login(string returnUrl = "/")
        {
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                // Indicate here where Auth0 should redirect the user after a login.
                // Note that the resulting absolute Uri must be added to the
                // **Allowed Callback URLs** settings for the app.
                .WithRedirectUri(returnUrl)
                .Build();

            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        }

        // GET: /Account/Profile
        [Authorize]
        public IActionResult Profile()
        {
            // attr init
            string name = string.Empty;
            string nickname = string.Empty;
            string userId = string.Empty;

            // Cast User Claims to List
            var user_identities = User.Claims.ToList();

            // Iterate Through user's identities to get "name" and "nickname"
            bool has_name = false;
            bool has_nickname = false;
            for (int i = 0; i < user_identities.Count; i++) {
                if (user_identities[i].Type.ToString() == "nickname") {
                    nickname = user_identities[i].Value.ToString();
                    has_nickname = true;
                }
                else if (user_identities[i].Type == "name") {
                    name = user_identities[i].Value.ToString();
                    has_name = true;
                }
            }

            // Get user ID
            var provider_and_userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            try {
                string[] parts = provider_and_userId.Split('|');
                userId = parts[1];
            }
            catch {
                return NotFound();
            }

            var ViewModel = new UserProfileViewModel {
                UserId = userId,
                Name = nickname,
                Email = name
            };

            return View(ViewModel);
        }

        // ACTION: /Account/Logout
        [Authorize]
        public async Task Logout()
        {
            var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
                // Indicate here where Auth0 should redirect the user after a logout.
                // Note that the resulting absolute Uri must be added to the
                // **Allowed Logout URLs** settings for the app.
                .WithRedirectUri(Url.Action("Index", "Home"))
                .Build();

            await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        ////////////////////////////////////////////////////////////////////////////////
        // Account Edit Functions
        ////////////////////////////////////////////////////////////////////////////////

        // GET: Redirect after account creation to /Account/Edit
        [Authorize]
        public async Task<IActionResult> Edit() {
            if (_context.Clients == null)
                return NotFound();

            // Get user ID
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return NotFound();

            // get client
            var client = await _context.Clients.FindAsync(userId);
            if (client == null)
                return NotFound();

            return View(client);
        }

        // POST: Update Client Information
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit([Bind("Id,FirstName,LastName,Phone")] Client client) {
            // initial null check
            if (_context.Clients == null)
                return NotFound();

            // Get user ID
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null || userId != client.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Profile));
            }
            return View(client);
        }

        ////////////////////////////////////////////////////////////////////////////////

        private bool ClientExists(string id)
        {
          return (_context.Clients?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}