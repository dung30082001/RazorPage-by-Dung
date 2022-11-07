using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyRazorPage.Models;
using System.Data;

namespace MyRazorPage.Product
{
    [Authorize(Roles = "1")]
    public class UpdateModel : PageModel
    {
        public List<Category> categories { get; set; }
        [BindProperty]
        public Models.Product Product { get; set; }

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
            Product = _dbContext.Products.Find(Convert.ToInt32(id));
            categories = _dbContext.Categories.ToList();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            categories = _dbContext.Categories.ToList();
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _dbContext.Update(Product);
            await _dbContext.SaveChangesAsync();
            return RedirectToPage("Display");
        }
    }
}

