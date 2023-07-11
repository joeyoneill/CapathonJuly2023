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
            Console.WriteLine();

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
    }
}