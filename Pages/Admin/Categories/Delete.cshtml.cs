using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MyRazorPage.Hubs;
using MyRazorPage.Models;
using System.Data;

namespace MyRazorPage.Pages.Admin.Categories
{
    [Authorize(Roles = "1")]
    public class DeleteModel : PageModel
    {
        private readonly IHubContext<HubServer> _hubContext;
        public Models.Category category { get; set; }

        private readonly PRN221_DBContext _dbContext;
        public DeleteModel(PRN221_DBContext dbContext, IHubContext<HubServer> hubContext)
        {
            _dbContext = dbContext;
            _hubContext = hubContext;
        }

        public async Task<IActionResult> OnGet(string id)
        {
            if (ModelState.IsValid)
            {
                category = _dbContext.Categories.Find(Convert.ToInt32(id));
                _dbContext.Categories.Remove(category);
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
