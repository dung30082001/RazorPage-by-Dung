using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyRazorPage.Models;
using System.Data;

namespace MyRazorPage.Pages.Admin
{
    [Authorize(Roles = "1")]
    public class Order_DetailModel : PageModel
    {
        public List<OrderDetail> od;

        public Order order;

        private readonly PRN221_DBContext _dbContext;

        public Order_DetailModel(PRN221_DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> OnGet(String id)
        {
            order = _dbContext.Orders.Find(Convert.ToInt32(id));
            od = _dbContext.OrderDetails.Include(P => P.Product).Where(o => o.OrderId == Convert.ToInt32(id)).ToList();
            return Page();
        }
    }
}
