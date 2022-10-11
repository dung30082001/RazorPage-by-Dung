using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyRazorPage.Models;
using System.Text.Json;

namespace MyRazorPage.Pages.Account
{
    public class ProfileModel : PageModel
    {
        
        public Models.Account accounts { get; set; }

        private readonly PRN221_DBContext _dbContext;
        
        public Models.Customer customer { get; set; }
        [BindProperty]
        public Models.Customer Customers { get; set; }
        [BindProperty]
        public Models.Account Account { get; set; }
        public ProfileModel(PRN221_DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void OnGet()
        {
            accounts = JsonSerializer.Deserialize<Models.Account>(HttpContext.Session.GetString("CustSession"));
            Account = _dbContext.Accounts.Find(accounts.AccountId);
            customer = _dbContext.Customers.FirstOrDefault(x => x.CustomerId.Equals(accounts.CustomerId));
            Customers = _dbContext.Customers.FirstOrDefault(x => x.CustomerId.Equals(customer.CustomerId));
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                _dbContext.Accounts.Update(Account);
                _dbContext.Customers.Update(Customers);
                await _dbContext.SaveChangesAsync();
                return RedirectToPage("/Index");
            }
            return Page();
        }
    }
}
