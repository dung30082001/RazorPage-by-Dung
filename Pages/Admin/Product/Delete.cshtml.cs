using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MyRazorPage.Hubs;
using MyRazorPage.Models;
using System.Data;

namespace MyRazorPage.Pages.Product
{
    [Authorize(Roles = "1")]
    public class DeleteModel : PageModel
    {
        private readonly IHubContext<HubServer> _hubContext;
        public Models.Product product { get; set; }

        private readonly PRN221_DBContext _dbContext;
        public DeleteModel(PRN221_DBContext dbContext, IHubContext<HubServer> hubContext)
        {
            _dbContext = dbContext;
            _hubContext = hubContext;
        }

        public async Task<IActionResult> OnGet(string id)
        {
            int ID = (int)HttpContext.Session.GetInt32("Role");
            if (ID == 2)
            {
                return RedirectToPage("/Error");
            }
            if (ModelState.IsValid)
            {
                product = _dbContext.Products.Find(Convert.ToInt32(id));
                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();
                await _hubContext.Clients.All.SendAsync("ReloadProduct");
                return RedirectToPage("Display");
            }
            else
            {
                return Page();
            }
        }
    }
}
