using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyRazorPage.Models;
using System.Data;
using System.Security.Claims;

namespace MyRazorPage.Pages
{
    public class IndexModel : PageModel
    {
        private readonly PRN221_DBContext _context;

        public List <Category> Categories { get; set; }

        public List <Models.Product> LProducts { get; set; }

        public int totalProducts { get; set; }

        public int pageNo { get; set; }
        [BindProperty]
        public string Search { get; set; }

        public int pageSize { get; set; }
        public IndexModel(PRN221_DBContext context)
        {
            _context = context;
        }
        public void OnGet(string id,int p = 1,int s = 10)
        {
            ClaimsIdentity identity = null;
            bool isAuthenticate = false;           
                identity = new ClaimsIdentity(new[]
            {
                    new Claim(ClaimTypes.Role,"2")
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                isAuthenticate = true;
            if (isAuthenticate)
            {
                var principle = new ClaimsPrincipal(identity);
                var signin = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle);
            }
                
                if (id != null)
            {
                Categories = _context.Categories.ToList();
                LProducts = _context.Products.Where(x => x.CategoryId == Convert.ToInt32(id)).ToList();
                pageSize = s;
                totalProducts = _context.Products.Where(x => x.CategoryId == Convert.ToInt32(id)).Count();
                pageNo = p;
            }
            else
            {
                Categories = _context.Categories.ToList();
                LProducts = _context.Products.Skip((p - 1) * s).Take(s).ToList();
                pageSize = s;
                totalProducts = _context.Products.Count();
                pageNo = p;
            }
        }
        public void OnPost(int p = 1, int s = 10)
        {
            Categories = _context.Categories.ToList();
            var price = Request.Form["Price"];
            Console.WriteLine(price);
            if (price.ToString().Equals("DESC"))
            {
                LProducts = _context.Products.Skip((p - 1) * s).Take(s).OrderByDescending(x => x.UnitPrice).ToList();
                pageSize = s;
                totalProducts = _context.Products.OrderByDescending(x => x.UnitPrice).ToList().Count();
                pageNo = p;
            }
            else if (price.ToString().Equals("ASC"))
            {
                LProducts = _context.Products.Skip((p - 1) * s).Take(s).OrderBy(x => x.UnitPrice).ToList();
                pageSize = s;
                totalProducts = _context.Products.OrderBy(x => x.UnitPrice).ToList().Count();
                pageNo = p;
            }
        }
        public IActionResult OnPostSearch(string id, int p = 1, int s = 10)
        {
            Categories = _context.Categories.ToList();
            LProducts = _context.Products.Where(z=>z.ProductName.Contains(Search)).Skip((p - 1) * s).Take(s).ToList();
            pageSize = s;
            totalProducts = _context.Products.Where(z => z.ProductName.Contains(Search)).Count();
            pageNo = p;
            return Page();
        }
        }
}

