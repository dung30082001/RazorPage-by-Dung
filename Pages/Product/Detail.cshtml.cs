using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyRazorPage.Models;
using System.Text.Json;

namespace MyRazorPage.Pages.Product
{
    public class DetailModel : PageModel
    {
        private readonly PRN221_DBContext _dbContext;

        public DetailModel(PRN221_DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Models.Product Products { get; set; }
        [BindProperty]
        public Models.Product product { get; set; }

        public int Id { get; set; }

        public void OnGet(String id)
        {
            if (id == null)
            {
                id = "1";
            }
            Products = _dbContext.Products.Find(Convert.ToInt32(id));
            //if ((int)HttpContext.Session.GetInt32("Id") != null)
            //{
            //    Id = (int)HttpContext.Session.GetInt32("Id");
            //}
            //else
            //{
            //    return;
            //}
        }
    }
}
