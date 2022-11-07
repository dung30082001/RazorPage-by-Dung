using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyRazorPage.Models;
using System.Drawing;
using System.Security.Claims;
using System.Text.Json;

namespace MyRazorPage.Pages.Account
{
    public class SignInModel : PageModel
    {
        private readonly PRN221_DBContext dBContext;
        public SignInModel(PRN221_DBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        [BindProperty]
        public Models.Account Account { get; set; }

        public int Role { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                Encryption en = new Encryption();
                string newP = en.Encrypt(Account.Password);
                var acc = await dBContext.Accounts.SingleOrDefaultAsync(a => a.Email.Equals(Account.Email) && a.Password.Equals(newP));
                ClaimsIdentity identity = null;
                bool isAuthenticate = false;
                if (acc != null)
                {
                    identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Role,acc.Role.ToString())
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                    isAuthenticate = true;
                    Role = (int)acc.Role;
                    if (isAuthenticate)
                    {
                        var principle = new ClaimsPrincipal(identity);
                        var signin = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle);
                    }
                    HttpContext.Session.SetString("cuID",acc.CustomerId);
                    HttpContext.Session.SetString("CustSession", JsonSerializer.Serialize(acc));
                    HttpContext.Session.SetInt32("Role", Role);
                    if(Role == 1)
                    {
                        return RedirectToPage("/Admin/Order");
                    }
                    return RedirectToPage("/index");
                }
                else
                {
                    ViewData["msg"] = "This account does not exist";
                    return Page();
                }
            }
            else
            {
                return Page();
            }
        }
    }
}
