using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using LearnASPNETCoreRazorPagesWithRealApps.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyRazorPage.Models;


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
        public Order Order { get; set; }
        public OrderDetail ODetail { get; set; }
        public double Total { get; set; }
        public int Size = 0;
        public CartModel(PRN221_DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void OnGet(string id)
        {
            if(JsonSerializer.Deserialize<Models.Account>(HttpContext.Session.GetString("CustSession")) == null)
            {
                return;
            }
            if (JsonSerializer.Deserialize<Models.Account>(HttpContext.Session.GetString("CustSession")) != null)
            { 
                accounts = JsonSerializer.Deserialize<Models.Account>(HttpContext.Session.GetString("CustSession"));
            }
            DCustomer = _dbContext.Customers.Find(accounts.CustomerId);
            listP = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            Total = listP.Sum(i => Convert.ToDouble(i.Product.UnitPrice) * i.Quantity);
        }
        public IActionResult OnGetBuyNow(string id)
        {
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
            listP = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            Total = listP.Sum(i => Convert.ToDouble(i.Product.UnitPrice) * i.Quantity);
            accounts = JsonSerializer.Deserialize<Models.Account>(HttpContext.Session.GetString("CustSession"));
            if (accounts==null)
            {
                await _dbContext.Customers.AddAsync(customer);
                await _dbContext.SaveChangesAsync();
            }
            Order = new Order();
            if (accounts == null)
            {
                Order.CustomerId = DCustomer.CustomerId;
            }
            else
            {
                Order.CustomerId = "ERNSH";
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
            return RedirectToPage("Cart");
            
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
