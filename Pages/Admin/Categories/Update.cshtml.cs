using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyRazorPage.Models;
using System.Data;

namespace MyRazorPage.Pages.Admin.Categories
{
    [Authorize(Roles = "1")]
    public class UpdateModel : PageModel
    {
        [BindProperty]
        public Models.Category Category { get; set; }

        private readonly PRN221_DBContext _dbContext;

        public UpdateModel(PRN221_DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> OnGet(string id)
        {
            int ID = (int)HttpContext.Session.GetInt32("Role");
            if (ID == 2)
            {
                return RedirectToPage("/Error");
            }
            Category = _dbContext.Categories.Find(Convert.ToInt32(id));
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            _dbContext.Categories.Update(Category);
            await _dbContext.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}

