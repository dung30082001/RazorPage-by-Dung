using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyRazorPage.Models;

namespace MyRazorPage.Pages
{
    public class IndexModel : PageModel
    {
        private readonly PRN221_DBContext _context;

        public IEnumerable<Category> Categories { get; set; }

        public int totalProduct { get; set; }

        public int pageNo { get; set; }

        public int pageSize { get; set; }
        public IEnumerable<Models.Product> LProducts { get; set; }
        public IndexModel(PRN221_DBContext context)
        {
            _context = context;
        }
        public void OnGet(String id, int p = 1, int s = 10)
        {
            if (id != null)
            {
                Categories = _context.Categories;
                LProducts = _context.Products.Where(x => x.CategoryId == Convert.ToInt32(id));
                pageSize = s;
                totalProduct = _context.Products.Where(x => x.CategoryId == Convert.ToInt32(id)).Count();
                pageNo = p;
            }
            else
            {
                Categories = _context.Categories;
                LProducts = _context.Products.Skip((p - 1) * s).Take(s).ToList();
                pageSize = s;
                totalProduct = _context.Products.Count();
                pageNo = p;
            }
        }
        public void OnPost(int p = 1, int s = 10)
        {
            Categories = _context.Categories;
            var price = Request.Form["Price"];
            Console.WriteLine(price);
            if (price.ToString().Equals("DESC"))
            {
                LProducts = _context.Products.OrderByDescending(x => x.UnitPrice);
                pageSize = s;
                totalProduct = _context.Products.Count();
                pageNo = p;
            }
            else if (price.ToString().Equals("ASC"))
            {
                LProducts = _context.Products.OrderBy(x => x.UnitPrice);
                pageSize = s;
                totalProduct = _context.Products.Count();
                pageNo = p;
            }
        }
    }
}

