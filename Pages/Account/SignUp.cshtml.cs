 using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyRazorPage.Models;

namespace MyRazorPage.Pages.Account
{
    public class SignUpModel : PageModel
    {
        [BindProperty]
        public Models.Account Account { get; set; }

        public List<Employee> employees { get; set; }

        public List<Customer> customers { get; set; }

        public PRN221_DBContext _context;

        public SignUpModel(PRN221_DBContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            employees = _context.Employees.ToList();
            customers = _context.Customers.ToList();
        }

        public void OnPost()
        {
            employees = _context.Employees.ToList();
            customers = _context.Customers.ToList();
            Encryption en = new Encryption();
            string newP = en.Encrypt(Account.Password);
            Account.Password = newP;
            _context.Accounts.Add(Account);
            _context.SaveChanges();
            ViewData["mess"] = "Add success";
        }
    }
}
