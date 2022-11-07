using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Security.Claims;
using System.Security.Principal;

namespace MyRazorPage.Pages.Account
{
    public class SignOutModel : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            HttpContext.Session.Clear();
            ClaimsIdentity identity = null;
            bool isAuthenticate = false;
            identity = new ClaimsIdentity(new[]
            {
                    new Claim(ClaimTypes.Role,"2")
                }, CookieAuthenticationDefaults.AuthenticationScheme);
            isAuthenticate = true;
            if (isAuthenticate)
            {
                var principle = new ClaimsPrincipal(identity);
                var signin = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle);
            }
            return RedirectToPage("/index");
        }
    }
}
