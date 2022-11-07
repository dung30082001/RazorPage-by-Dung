using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MyRazorPage.Hubs;
using MyRazorPage.Models;
using System;
using System.Data;

namespace MyRazorPage.Pages.Product
{
    [Authorize(Roles = "1")]
    public class CreateModel : PageModel
    {
        private readonly IHubContext<HubServer> _hubContext;
        public List<Category> categories { get; set; }
        [BindProperty]
        public Models.Product product { get; set; } 

        public List<Models.Product> Lproducts { get; set; }

        private readonly PRN221_DBContext _dbContext;

        public CreateModel(PRN221_DBContext dbContext, IHubContext<HubServer> hubContext)
        {
            _dbContext = dbContext;
            _hubContext = hubContext;
        }
        public async Task<IActionResult> OnGet()
        {
            int ID = (int)HttpContext.Session.GetInt32("Role");
            if (ID == 2)
            {
                return RedirectToPage("/Error");
            }
            categories = _dbContext.Categories.ToList();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            categories = _dbContext.Categories.ToList();
            Lproducts = _dbContext.Products.ToList();
            foreach (var item in Lproducts)
            {
                if (product.ProductName.Equals(item.ProductName))
                {
                    ViewData["alert"] = "The productName already exits";
                    categories = _dbContext.Categories.ToList();
                    return Page();
                }
            }
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("ReloadProduct");
            return RedirectToPage("Display");
        }
    }
}
