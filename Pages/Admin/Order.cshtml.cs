using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyRazorPage.Models;
using System.Data;

namespace MyRazorPage.Pages.Admin
{
    [Authorize(Roles = "1")]
    public class OrderModel : PageModel
    {
        public int totalOrders{ get; set; }

        public int pageNo { get; set; }

        public int pageSize { get; set; }

        public List<Order> orders { get; set; }

        private readonly PRN221_DBContext _dbContext;

        public OrderModel(PRN221_DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> OnGet(int p = 1, int s = 5)
        {
            orders = _dbContext.Orders.Include(e => e.Employee).Skip((p - 1) * s).Take(s).ToList(); ;
            pageSize = s;
            totalOrders = _dbContext.Orders.ToList().Count();
            pageNo = p;
            return Page();
        }
        public void OnPost(int p = 1, int s = 100)
        {
            var from = Request.Form["from"];
            var to = Request.Form["to"];
            orders = _dbContext.Orders.Where(o => o.OrderDate >= Convert.ToDateTime(from) && o.OrderDate <= Convert.ToDateTime(to)).Include(e => e.Employee).Skip((p - 1) * s).Take(s).ToList();
            pageSize = s;
            pageNo = p;
        }
    }
}
