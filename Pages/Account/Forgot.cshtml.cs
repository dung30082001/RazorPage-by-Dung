using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyRazorPage.Models;
using System.Net.Mail;

namespace MyRazorPage.Pages.Account
{
    public class ForgotModel : PageModel
    {
        PRN221_DBContext _context;

        public List<Models.Account> listA { get; set; }
        [BindProperty]
        public string Email { get; set; }

        public Models.Account account { get; set; }

        public string Alert { get; set; }

        public ForgotModel(PRN221_DBContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            listA = _context.Accounts.ToList();
            foreach(var item in listA)
            {
                if (Email.Equals(item.Email))
                {
                    MailMessage mm = new MailMessage();
                    mm.To.Add(Email);
                    mm.Subject = "This is forgot password";
                    mm.Body = "Password has been changed to 456";
                    mm.IsBodyHtml = false;
                    mm.From = new MailAddress("dungndhe150788@fpt.edu.vn");
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new System.Net.NetworkCredential("dungndhe150788@fpt.edu.vn", "anhdungzoo9");
                    await smtp.SendMailAsync(mm);
                    account = _context.Accounts.First(x => x.Email.Equals(Email));
                    Encryption en = new Encryption();
                    string newP = en.Encrypt("456");
                    account.Password = newP;
                    _context.Accounts.Update(account);
                    await _context.SaveChangesAsync();
                    ViewData["Alert"] = "The email has been send to "+Email;
                    return Page();
                }
            }
            ViewData["Alert"] = "Your email does not exits";
            return Page();
        }

    }
}
