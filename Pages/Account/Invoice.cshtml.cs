using LearnASPNETCoreRazorPagesWithRealApps.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyRazorPage.Models;

namespace MyRazorPage.Pages.Account
{
    public class InvoiceModel : PageModel
    {
        public PRN221_DBContext _dbContext;
        public List<Item> listItem { get; set; }

        public string Total { get; set; }
        public void OnGet()
        {
            listItem = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            Total = HttpContext.Session.GetString("Total");
        }
        public void OnPost()
        {

        }
    }
}
