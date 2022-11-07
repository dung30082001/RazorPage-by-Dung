using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using LearnASPNETCoreRazorPagesWithRealApps.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyRazorPage.Models;
using System.Diagnostics;

namespace MyRazorPage.Pages.Account
{
    public class CartModel : PageModel
    {
        private readonly PRN221_DBContext _dbContext;
        public List<Item> listP { get; set; }
        [BindProperty]
        public Customer customer  { get; set; }

        public Models.Account accounts { get; set; }

        public Customer DCustomer { get; set; }

        public Customer RCustomer
        {
            get; set;
        }
        public Order Order { get; set; }
        public OrderDetail ODetail { get; set; }
        public double Total { get; set; }
        public int Size = 0;
        
        public string ID { get; set; }
        public CartModel(PRN221_DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void OnGet()
        {
            //accounts = JsonSerializer.Deserialize<Models.Account>(HttpContext.Session.GetString("CustSession"));
            //Id = (int)HttpContext.Session.GetInt32("Id");
            //if (Id!= 0)
            //{
            //DCustomer = _dbContext.Customers.Find(accounts.CustomerId);
            DCustomer = _dbContext.Customers.Find(HttpContext.Session.GetString("cusid"));
            //}
            //if (Id == 0)
            //{
            //    DCustomer = _dbContext.Customers.Find("XEAKQ");
            //}
            listP = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            Total = listP.Sum(i => Convert.ToDouble(i.Product.UnitPrice) * i.Quantity);
        }
        public IActionResult OnGetBuyNow(string id,string cusid)
        {
            //id = ID;
            HttpContext.Session.SetString("cusid",cusid);
            listP = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            if (listP == null)
            {
                listP = new List<Item>();
                listP.Add(new Item
                {
                    Product = _dbContext.Products.Find(Convert.ToInt32(id)),
                    Quantity = 1
                });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", listP);
                Size = 1;
                HttpContext.Session.SetString("Size", JsonSerializer.Serialize(Size));
            }
            else
            {
                int index = Exists(listP, id);
                if (index == -1)
                {
                    listP.Add(new Item
                    {
                        Product = _dbContext.Products.Find(Convert.ToInt32(id)),
                        Quantity = 1
                    });
                    Size = listP.Count;
                    HttpContext.Session.SetString("Size", JsonSerializer.Serialize(Size));
                }
                else
                {
                    listP[index].Quantity++;
                    Size = listP.Count;
                    HttpContext.Session.SetString("Size", JsonSerializer.Serialize(Size));
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", listP);
                HttpContext.Session.SetString("Size", JsonSerializer.Serialize(Size));
            }
            return RedirectToPage("Cart");
        }
        public IActionResult OnGetDelete(string id)
        {
            listP = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = Exists(listP, id);
            listP.RemoveAt(index);
            Size = listP.Count;
            HttpContext.Session.SetString("Size", JsonSerializer.Serialize(Size));
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", listP);
            return RedirectToPage("Cart");
        }
        private int Exists(List<Item> cart, string id)
        {
            for (var i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.ProductId.ToString() == id)
                {
                    return i;
                }
            }
            return -1;
        }
        public async Task<IActionResult> OnPost()
        {
            DCustomer = _dbContext.Customers.Find(HttpContext.Session.GetString("cusid"));
            listP = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            Total = listP.Sum(i => Convert.ToDouble(i.Product.UnitPrice) * i.Quantity);
            if (DCustomer.CustomerId.Equals("FOLIG"))
            {
                String id = GenerateCusID(5);
                customer.CustomerId = id;
                string date = DateTime.UtcNow.ToString("MM-dd-yyyy");
                customer.CreatedDate = Convert.ToDateTime(date);
                await _dbContext.Customers.AddAsync(customer);
                await _dbContext.SaveChangesAsync();
            }
            Order = new Order();
            if (!DCustomer.CustomerId.Equals("FOLIG"))
            {
                Order.CustomerId = DCustomer.CustomerId;
            }
            else
            {
                Order.CustomerId = "FOLIG";
            }
            await _dbContext.Orders.AddAsync(Order);
            await _dbContext.SaveChangesAsync();
            foreach (var item in listP)
            {
                ODetail = new OrderDetail();
                ODetail.OrderId = Order.OrderId;
                ODetail.ProductId = item.Product.ProductId;
                ODetail.UnitPrice = Convert.ToInt32(item.Product.UnitPrice);
                ODetail.Quantity = (short)item.Quantity;
                ODetail.Discount = 0;
                await _dbContext.OrderDetails.AddAsync(ODetail);
                await _dbContext.SaveChangesAsync();
            }
            //HttpContext.Session.Remove("cart");
            Size = 0;
            HttpContext.Session.SetString("Size", JsonSerializer.Serialize(Size));
            HttpContext.Session.SetString("Total", JsonSerializer.Serialize(Total));

            //PdfDocument document = new PdfDocument();
            //PdfPage page = document.AddPage();
            //XGraphics gfx = XGraphics.FromPdfPage(page);
            //XFont font = new XFont("Verdana", 20, XFontStyle.Bold);
            //gfx.DrawString("ProductName", new XFont("Arial", 15, XFontStyle.Bold),XBrushes.Black, new XPoint(100, 280));
            //gfx.DrawString("ProducPrice", new XFont("Arial", 15, XFontStyle.Bold), XBrushes.Black, new XPoint(350, 280));
            //gfx.DrawLine(new XPen(XColor.FromArgb(50, 30, 200)), new XPoint(50,290), new XPoint(550, 290));
            //int currentYposition_value = 303;
            //int currentYposition_line = 310;
            //foreach (var item in listP)
            //{
            //    gfx.DrawString(item.Product.ProductName, new XFont("Arial", 15, XFontStyle.Bold), XBrushes.Black, new XPoint(100, currentYposition_value));
            //    gfx.DrawString(item.Product.UnitPrice.ToString(), new XFont("Arial", 15, XFontStyle.Bold), XBrushes.Black, new XPoint(350, currentYposition_value));
            //    gfx.DrawLine(new XPen(XColor.FromArgb(50, 30, 200)), new XPoint(50, currentYposition_line), new XPoint(550, currentYposition_line));
            //    currentYposition_value += 20;
            //    currentYposition_line += 20;
            //}
            //gfx.DrawString(Total.ToString(), new XFont("Arial", 15, XFontStyle.Bold), XBrushes.Black, new XPoint(350, currentYposition_value));
            //string filename = "E:\\TestPDF.pdf";
            //document.Save(filename);
            return RedirectToPage("Invoice");
            
        }
        private string GenerateCusID(int length)
        {
            // creating a StringBuilder object()
            StringBuilder str_build = new StringBuilder();
            Random random = new Random();

            char letter;

            for (int i = 0; i < length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }
            return str_build.ToString();
        }
    }
}
