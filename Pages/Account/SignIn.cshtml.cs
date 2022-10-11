using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyRazorPage.Models;
using System.Drawing;
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

        public int Id { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var acc = await dBContext.Accounts.SingleOrDefaultAsync(a => a.Email.Equals(Account.Email) && a.Password.Equals(Account.Password));
                if (acc != null)
                {
                    Id = acc.AccountId;
                    HttpContext.Session.SetInt32("Id", Id);
                    HttpContext.Session.SetString("CustSession", JsonSerializer.Serialize(acc));
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
