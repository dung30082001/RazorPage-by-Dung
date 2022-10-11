using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyRazorPage.Pages.Account
{
    public class SignOutModel : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/index");
        }
    }
}
