using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyRazorPage.Models;
using System.Text.Json;

namespace MyRazorPage.Pages.Product
{
    public class DetailModel : PageModel
    {
        private readonly PRN221_DBContext _dbContext;

        public DetailModel(PRN221_DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Models.Account? Accounts { get; set; }

        public Models.Customer customer { get; set; }
        public Models.Product Products { get; set; }
        [BindProperty]
        public Models.Product product { get; set; }


        public void OnGet(String id)
        {
            if (id == null)
            {
                id = "1";
            }
            Products = _dbContext.Products.Find(Convert.ToInt32(id));
            customer = _dbContext.Customers.Find(HttpContext.Session.GetString("cuID"));
            if(customer == null)
            {
                customer = _dbContext.Customers.Find("FOLIG");
            }
        }
    }
}
