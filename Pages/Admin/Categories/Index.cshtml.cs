using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyRazorPage.Models;
using System.Data;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace MyRazorPage.Pages.Admin.Categories
{
    [Authorize(Roles = "1")]
    public class IndexModel : PageModel
    {
        public Models.PRN221_DBContext _dbcontext;

        public List<Category> categories;

        public int totalCategories { get; set; }

        private IHostingEnvironment _environment;

        public int pageNo { get; set; }

        public int pageSize { get; set; }

        public IndexModel(IHostingEnvironment environment,Models.PRN221_DBContext dbcontext)
        {
            _environment = environment;
            _dbcontext = dbcontext;
        }
        public async Task<IActionResult> OnGet(int p = 1, int s = 5)
        {
            pageSize = s;
            totalCategories = _dbcontext.Categories.ToList().Count();
            pageNo = p;
            categories = _dbcontext.Categories.OrderByDescending(x => x.CategoryId).Skip((p - 1) * s).Take(s).ToList();
            return Page();
        }
    }
}
