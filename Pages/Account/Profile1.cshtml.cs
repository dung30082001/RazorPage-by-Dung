using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using MyRazorPage.Models;
using System.Text.Json;

namespace MyRazorPage.Pages.Account
{
    public class Profile1Model : PageModel
    {
        private readonly PRN221_DBContext _dbContext;

        public Models.Account accounts { get; set; }

        public List<Order> orders { get; set; }
        [BindProperty]
        public List<OrderDetail> detail { get; set; }
        [BindProperty]
        public Order Order { get; set; }


        public Profile1Model(PRN221_DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void OnGet(String id)
        {
            accounts = JsonSerializer.Deserialize<Models.Account>(HttpContext.Session.GetString("CustSession"));
            orders = _dbContext.Orders.Where(x => x.CustomerId.Equals(accounts.CustomerId)).ToList();
            if (id != null)
            {
                detail = _dbContext.OrderDetails.Where(x => x.OrderId == Convert.ToInt32(id)).ToList();
            }
            Order = _dbContext.Orders.Find(Convert.ToInt32(id));
        }
    }
}
