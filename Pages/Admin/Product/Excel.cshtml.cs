using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyRazorPage.Models;
using System.Text;

namespace MyRazorPage.Pages.Admin.Product
{
    public class ExcelModel : PageModel
    {
        public PRN221_DBContext _context;

        public List<Models.Product> listP;

        public ExcelModel(PRN221_DBContext context)
        {
            _context = context;
        }
        public FileResult OnGet()
        {
            string[] columnsNames = new string[] { "ProductId", "ProductName", "CategoryId", "Quantity", "UnitPrice", "UnitStock", "UnitOrder", "ReOrderLevel", "Discontinueted" };
            listP = _context.Products.ToList();
            string csv = string.Empty;
            foreach (string columnName in columnsNames)
            {
                csv += columnName + ',';
            }
            csv += "\r\n";
            foreach(var products in listP){
                csv += products.ProductId.ToString().Replace(",", ";") + ',';
                csv += products.ProductName.Replace(",", ";") + ',';
                csv += products.CategoryId.ToString().Replace(",", ";") + ',';
                csv += products.QuantityPerUnit.ToString().Replace(",", ";") + ',';
                csv += products.UnitPrice.ToString().Replace(",", ";") + ',';
                csv += products.UnitsInStock.ToString().Replace(",", ";") + ',';
                csv += products.UnitsOnOrder.ToString().Replace(",", ";") + ',';
                csv += products.ReorderLevel.ToString().Replace(",", ";") + ',';
                csv += products.Discontinued.ToString().Replace(",", ";") + ',';
                csv += "\r\n";
            }
            byte[] bytes = Encoding.ASCII.GetBytes(csv);
            return File(bytes, "text/csv", "Products.csv");
        }
    }
}
