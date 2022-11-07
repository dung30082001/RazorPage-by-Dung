using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MyRazorPage.Hubs;
using MyRazorPage.Models;
using System;
using System.Data;

namespace MyRazorPage.Pages.Admin.Categories
{
    [Authorize(Roles = "1")]
    public class CreateModel : PageModel
    {
        private readonly IHubContext<HubServer> _hubContext;
        public List<Category> Lcategories { get; set; }
        [BindProperty]
        public Category category { get; set; }


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
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            Lcategories = _dbContext.Categories.ToList();
            foreach (var item in Lcategories)
            {
                if (category.CategoryName.Equals(item.CategoryName))
                { 
                    HttpContext.Session.SetString("alert", "The category name already exits");
                    return Page();
                }
            }
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("ReloadProduct");
            return RedirectToPage("Index");
        }
    }
}
