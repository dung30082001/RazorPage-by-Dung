using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyRazorPage.Models;
using System.Text;

namespace MyRazorPage.Pages.Admin
{
    public class ExcelModel : PageModel
    {
        public PRN221_DBContext _context;

        public List<Models.Order> listO;

        public ExcelModel(PRN221_DBContext context)
        {
            _context = context;
        }
        public FileResult OnGet()
        {
            string[] columnsNames = new string[] { "OrderID", "CustomerID", "EmployeeID", "OrderDate", "RequiredDate", "Freight", "ShipName", "ShipAddress", "ShipCity","ShipPostalCode","ShipCountry" };
            listO = _context.Orders.ToList();
            string csv = string.Empty;
            foreach (string columnName in columnsNames)
            {
                csv += columnName + ',';
            }
            csv += "\r\n";
            foreach(var products in listO){
                csv += products.OrderId.ToString().Replace(",", ";") + ',';
                csv += products.CustomerId.ToString().Replace(",", ";") + ',';
                csv += products.EmployeeId.ToString().Replace(",", ";") + ',';
                csv += Convert.ToDateTime(products.OrderDate).ToString().Replace(",", ";") + ',';
                csv += Convert.ToDateTime(products.RequiredDate).ToString().Replace(",", ";") + ',';
                csv += products.Freight.ToString().Replace(",", ";") + ',';
                if (products.ShipName == null)
                {
                    csv += "NULL";
                }
                else
                {
                csv += products.ShipName.ToString().Replace(",", ";") + ',';
                }
                if (products.ShipAddress == null)
                {
                    csv += "NULL";
                }
                else
                {
                    csv += products.ShipAddress.ToString().Replace(",", ";") + ',';
                }
                if (products.ShipCity == null)
                {
                    csv += "NULL";
                }
                else
                {
                    csv += products.ShipCity.ToString().Replace(",", ";") + ',';
                }
                if (products.ShipPostalCode == null)
                {
                    csv += "NULL";
                }
                else
                {
                    csv += products.ShipPostalCode.ToString().Replace(",", ";") + ',';
                }
                if (products.ShipCountry == null)
                {
                    csv += "NULL";
                }
                else
                {
                    csv += products.ShipCountry.ToString().Replace(",", ";") + ',';
                }
                csv += "\r\n";
            }
            byte[] bytes = Encoding.ASCII.GetBytes(csv);
            return File(bytes, "text/csv", "Order.csv");
        }
    }
}
