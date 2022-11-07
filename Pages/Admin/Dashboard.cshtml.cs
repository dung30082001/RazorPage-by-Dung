using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyRazorPage.Models;
using System.Data;

namespace MyRazorPage.Pages.Admin
{
    [Authorize(Roles = "1")]
    public class DashboardModel : PageModel
    {
        private readonly PRN221_DBContext _dbContext;

        public decimal totalOrders { get; set; }

        public int totalGuest { get; set; }

        public decimal weekSale { get; set; }

        public int totalCustomers { get; set; }

        public List<Order> orders { get; set; }

        public List<Order> orders1 { get; set; }

        public List<Order> orders2 { get; set; }

        public List<Order> orders3 { get; set; }

        public List<Order> orders4 { get; set; }

        public List<Order> orders5 { get; set; }

        public List<Order> orders6 { get; set; }

        public List<Order> orders7 { get; set; }

        public List<Order> orders8 { get; set; }

        public List<Order> orders9 { get; set; }

        public List<Order> orders10 { get; set; }

        public List<Order> orders11 { get; set; }

        public List<Order> orders12 { get; set; }

        public List<OrderDetail> ordersD { get; set; }

        public List<OrderDetail> ordersd1 { get; set; }

        public List<OrderDetail> ordersd2 { get; set; }

        public List<OrderDetail> ordersd3 { get; set; }

        public List<OrderDetail> ordersd4 { get; set; }

        public List<OrderDetail> ordersd5 { get; set; }

        public List<OrderDetail> ordersd6 { get; set; }

        public List<OrderDetail> ordersd7 { get; set; }

        public List<OrderDetail> ordersd8 { get; set; }

        public List<OrderDetail> ordersd9 { get; set; }

        public List<OrderDetail> ordersd10 { get; set; }

        public List<OrderDetail> ordersd11 { get; set; }

        public List<OrderDetail> ordersd12 { get; set; }

        public List<OrderDetail> ordersd { get; set; }
        public List<Customer> customers { get; set; }


        public decimal Month1 { get; set; }

        public decimal Month2 { get; set; }

        public decimal Month3 { get; set; }

        public decimal Month4 { get; set; }

        public decimal Month5 { get; set; }

        public decimal Month6 { get; set; }

        public decimal Month7 { get; set; }

        public decimal Month8 { get; set; }

        public decimal Month9 { get; set; }

        public decimal Month10 { get; set; }

        public decimal Month11 { get; set; }

        public decimal Month12 { get; set; }

        public DashboardModel(PRN221_DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> OnGet()
        {
            int ID = (int)HttpContext.Session.GetInt32("Role");
            if (ID == 2)
            {
                return RedirectToPage("/Error");
            }
            totalOrders = 0;
            ordersD = _dbContext.OrderDetails.ToList();
            foreach (var item in ordersD)
            {
                totalOrders += item.UnitPrice;
            }
            customers = _dbContext.Customers.GroupBy(cus => cus.CustomerId).Select(x => x.First()).ToList();
            totalGuest = _dbContext.Customers.Where(c => c.CreatedDate != null).Count();
            totalCustomers = customers.Count;
            orders = _dbContext.Orders.Where(x => x.OrderId >= 10816).ToList();
            foreach (var item in orders)
            {
                ordersd = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                foreach (var od in ordersd)
                {
                    weekSale += od.UnitPrice;
                }
            }
            orders1 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1997-01-01") && x.OrderDate <= Convert.ToDateTime("1997-01-30")).ToList();
            foreach (var item in orders1)
            {
                ordersd1 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                foreach (var od in ordersd1)
                {
                    Month1++;
                }
            }
            orders2 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1997-02-01") && x.OrderDate <= Convert.ToDateTime("1997-02-28")).ToList();
            foreach (var item in orders2)
            {
                ordersd2 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                foreach (var od in ordersd2)
                {
                    Month2++;
                }
            }
            orders3 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1997-03-01") && x.OrderDate <= Convert.ToDateTime("1997-03-30")).ToList();
            foreach (var item in orders3)
            {
                ordersd3 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                foreach (var od in ordersd3)
                {
                    Month3++;
                }
            }
            orders4 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1997-04-01") && x.OrderDate <= Convert.ToDateTime("1997-04-30")).ToList();
            foreach (var item in orders4)
            {
                ordersd4 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                foreach (var od in ordersd4)
                {
                    Month4++;
                }
            }
            orders5 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1997-05-01") && x.OrderDate <= Convert.ToDateTime("1997-05-30")).ToList();
            foreach (var item in orders5)
            {
                ordersd5 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                foreach (var od in ordersd5)
                {
                    Month5++;
                }
            }
            orders6 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1997-06-01") && x.OrderDate <= Convert.ToDateTime("1997-06-30")).ToList();
            foreach (var item in orders6)
            {
                ordersd6 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                foreach (var od in ordersd6)
                {
                    Month6++;
                }
            }
            orders7 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1997-07-01") && x.OrderDate <= Convert.ToDateTime("1997-07-30")).ToList();
            foreach (var item in orders7)
            {
                ordersd7 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                foreach (var od in ordersd7)
                {
                    Month7++;
                }
            }
            orders8 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1997-08-01") && x.OrderDate <= Convert.ToDateTime("1997-08-30")).ToList();
            foreach (var item in orders8)
            {
                ordersd8 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                foreach (var od in ordersd8)
                {
                    Month8++;
                }
            }
            orders9 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1997-09-01") && x.OrderDate <= Convert.ToDateTime("1997-09-30")).ToList();
            foreach (var item in orders9)
            {
                ordersd9 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                foreach (var od in ordersd9)
                {
                    Month9++;
                }
            }
            orders10 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1997-10-01") && x.OrderDate <= Convert.ToDateTime("1997-10-30")).ToList();
            foreach (var item in orders10)
            {
                ordersd10 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                foreach (var od in ordersd10)
                {
                    Month10++;
                }
            }
            orders11 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1997-11-01") && x.OrderDate <= Convert.ToDateTime("1997-11-30")).ToList();
            foreach (var item in orders11)
            {
                ordersd11 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                foreach (var od in ordersd11)
                {
                    Month11++;
                }
            }
            orders12 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1997-12-01") && x.OrderDate <= Convert.ToDateTime("1997-12-30")).ToList();
            foreach (var item in orders12)
            {
                ordersd12 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                foreach (var od in ordersd12)
                {
                    Month12++;
                }
            }
            return Page();
        }
        public void OnPost()
        {
            totalOrders = 0;
            var year = Request.Form["Year"];
            if (Convert.ToInt32(year) == 1996)
            {
                customers = _dbContext.Customers.GroupBy(cus => cus.CustomerId).Select(x => x.First()).ToList();
                totalGuest = _dbContext.Customers.Where(c => c.CreatedDate != null).Count();
                totalCustomers = customers.Count;
                orders = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1996-01-01") && x.OrderDate <= Convert.ToDateTime("1996-12-30")).ToList();
                foreach (var item in orders)
                {
                    ordersd = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                    foreach (var od in ordersd)
                    {
                        weekSale += od.UnitPrice;
                    }
                }
                orders1 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1996-01-01") && x.OrderDate <= Convert.ToDateTime("1996-01-30")).ToList();
                foreach (var item in orders1)
                {
                    ordersd1 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                    foreach (var od in ordersd1)
                    {
                        Month1++;
                    }
                }
                totalOrders += Month1;
                orders2 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1996-02-01") && x.OrderDate <= Convert.ToDateTime("1996-02-28")).ToList();
                foreach (var item in orders2)
                {
                    ordersd2 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                    foreach (var od in ordersd2)
                    {
                        Month2++;
                    }
                }
                totalOrders += Month2;
                orders3 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1996-03-01") && x.OrderDate <= Convert.ToDateTime("1996-03-30")).ToList();
                foreach (var item in orders3)
                {
                    ordersd3 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                    foreach (var od in ordersd3)
                    {
                        Month3++;
                    }
                }
                totalOrders += Month3;
                orders4 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1996-04-01") && x.OrderDate <= Convert.ToDateTime("1996-04-30")).ToList();
                foreach (var item in orders4)
                {
                    ordersd4 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                    foreach (var od in ordersd4)
                    {
                        Month4++;
                    }
                }
                totalOrders += Month4;
                orders5 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1996-05-01") && x.OrderDate <= Convert.ToDateTime("1996-05-30")).ToList();
                foreach (var item in orders5)
                {
                    ordersd5 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                    foreach (var od in ordersd5)
                    {
                        Month5++;
                    }
                }
                totalOrders += Month5;
                orders6 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1996-06-01") && x.OrderDate <= Convert.ToDateTime("1996-06-30")).ToList();
                foreach (var item in orders6)
                {
                    ordersd6 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                    foreach (var od in ordersd6)
                    {
                        Month6++;
                    }
                }
                totalOrders += Month6;
                orders7 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1996-07-01") && x.OrderDate <= Convert.ToDateTime("1996-07-30")).ToList();
                foreach (var item in orders7)
                {
                    ordersd7 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                    foreach (var od in ordersd7)
                    {
                        Month7++;
                    }
                }
                totalOrders += Month7;
                orders8 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1996-08-01") && x.OrderDate <= Convert.ToDateTime("1996-08-30")).ToList();
                foreach (var item in orders8)
                {
                    ordersd8 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                    foreach (var od in ordersd8)
                    {
                        Month8++;
                    }
                }
                totalOrders += Month8;
                orders9 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1996-09-01") && x.OrderDate <= Convert.ToDateTime("1996-09-30")).ToList();
                foreach (var item in orders9)
                {
                    ordersd9 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                    foreach (var od in ordersd9)
                    {
                        Month9++;
                    }
                }
                totalOrders += Month9;
                orders10 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1996-10-01") && x.OrderDate <= Convert.ToDateTime("1996-10-30")).ToList();
                foreach (var item in orders10)
                {
                    ordersd10 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                    foreach (var od in ordersd10)
                    {
                        Month10++;
                    }
                }
                totalOrders += Month10;
                orders11 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1996-11-01") && x.OrderDate <= Convert.ToDateTime("1996-11-30")).ToList();
                foreach (var item in orders11)
                {
                    ordersd11 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                    foreach (var od in ordersd11)
                    {
                        Month11++;
                    }
                }
                totalOrders += Month11;
                orders12 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1996-12-01") && x.OrderDate <= Convert.ToDateTime("1996-12-30")).ToList();
                foreach (var item in orders12)
                {
                    ordersd12 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                    foreach (var od in ordersd12)
                    {
                        Month12++;
                    }
                }
                totalOrders += Month12;
            }
            if (Convert.ToInt32(year) == 1997)
            {
                customers = _dbContext.Customers.GroupBy(cus => cus.CustomerId).Select(x => x.First()).ToList();
                totalGuest = _dbContext.Customers.Where(c => c.CreatedDate != null).Count();
                totalCustomers = customers.Count;
                orders = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1997-01-01") && x.OrderDate <= Convert.ToDateTime("1997-12-30")).ToList();
                foreach (var item in orders)
                {
                    ordersd = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                    foreach (var od in ordersd)
                    {
                        weekSale += od.UnitPrice;
                    }
                }
                orders1 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1997-01-01") && x.OrderDate <= Convert.ToDateTime("1997-01-30")).ToList();
                foreach (var item in orders1)
                {
                    ordersd1 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                    foreach (var od in ordersd1)
                    {
                        Month1++;
                    }
                }
                totalOrders += Month1;
                orders2 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1997-02-01") && x.OrderDate <= Convert.ToDateTime("1997-02-28")).ToList();
                foreach (var item in orders2)
                {
                    ordersd2 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                    foreach (var od in ordersd2)
                    {
                        Month2++;
                    }
                }
                totalOrders += Month2;
                orders3 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1997-03-01") && x.OrderDate <= Convert.ToDateTime("1997-03-30")).ToList();
                foreach (var item in orders3)
                {
                    ordersd3 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                    foreach (var od in ordersd3)
                    {
                        Month3++;
                    }
                }
                totalOrders += Month3;
                orders4 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1997-04-01") && x.OrderDate <= Convert.ToDateTime("1997-04-30")).ToList();
                foreach (var item in orders4)
                {
                    ordersd4 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                    foreach (var od in ordersd4)
                    {
                        Month4++;
                    }
                }
                totalOrders += Month4;
                orders5 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1997-05-01") && x.OrderDate <= Convert.ToDateTime("1997-05-30")).ToList();
                foreach (var item in orders5)
                {
                    ordersd5 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                    foreach (var od in ordersd5)
                    {
                        Month5++;
                    }
                }
                totalOrders += Month5;
                orders6 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1997-06-01") && x.OrderDate <= Convert.ToDateTime("1997-06-30")).ToList();
                foreach (var item in orders6)
                {
                    ordersd6 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                    foreach (var od in ordersd6)
                    {
                        Month6++;
                    }
                }
                totalOrders += Month6;
                orders7 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1997-07-01") && x.OrderDate <= Convert.ToDateTime("1997-07-30")).ToList();
                foreach (var item in orders7)
                {
                    ordersd7 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                    foreach (var od in ordersd7)
                    {
                        Month7++;
                    }
                }
                totalOrders += Month7;
                orders8 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1997-08-01") && x.OrderDate <= Convert.ToDateTime("1997-08-30")).ToList();
                foreach (var item in orders8)
                {
                    ordersd8 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                    foreach (var od in ordersd8)
                    {
                        Month8++;
                    }
                }
                totalOrders += Month8;
                orders9 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1997-09-01") && x.OrderDate <= Convert.ToDateTime("1997-09-30")).ToList();
                foreach (var item in orders9)
                {
                    ordersd9 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                    foreach (var od in ordersd9)
                    {
                        Month9++;
                    }
                }
                totalOrders += Month9;
                orders10 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1997-10-01") && x.OrderDate <= Convert.ToDateTime("1997-10-30")).ToList();
                foreach (var item in orders10)
                {
                    ordersd10 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                    foreach (var od in ordersd10)
                    {
                        Month10++;
                    }
                }
                totalOrders += Month10;
                orders11 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1997-11-01") && x.OrderDate <= Convert.ToDateTime("1997-11-30")).ToList();
                foreach (var item in orders11)
                {
                    ordersd11 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                    foreach (var od in ordersd11)
                    {
                        Month11++;
                    }
                }
                totalOrders += Month11;
                orders12 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1997-12-01") && x.OrderDate <= Convert.ToDateTime("1997-12-30")).ToList();
                foreach (var item in orders12)
                {
                    ordersd12 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                    foreach (var od in ordersd12)
                    {
                        Month12++;
                    }
                }
                totalOrders += Month12;
            }
            if (Convert.ToInt32(year) == 1998)
            {
                customers = _dbContext.Customers.GroupBy(cus => cus.CustomerId).Select(x => x.First()).ToList();
                totalGuest = _dbContext.Customers.Where(c => c.CreatedDate != null).Count();
                totalCustomers = customers.Count;
                orders = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1998-01-01") && x.OrderDate <= Convert.ToDateTime("1998-12-30")).ToList();
                foreach (var item in orders)
                {
                    ordersd = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                    foreach (var od in ordersd)
                    {
                        weekSale += od.UnitPrice;
                    }
                }
                orders1 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1998-01-01") && x.OrderDate <= Convert.ToDateTime("1998-01-30")).ToList();
                foreach (var item in orders1)
                {
                    ordersd1 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                    foreach (var od in ordersd1)
                    {
                        Month1++;
                    }
                }
                totalOrders += Month1;
                orders2 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1998-02-01") && x.OrderDate <= Convert.ToDateTime("1998-02-28")).ToList();
                foreach (var item in orders2)
                {
                    ordersd2 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                    foreach (var od in ordersd2)
                    {
                        Month2++;
                    }
                }
                totalOrders += Month2;
                orders3 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1998-03-01") && x.OrderDate <= Convert.ToDateTime("1998-03-30")).ToList();
                foreach (var item in orders3)
                {
                    ordersd3 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                    foreach (var od in ordersd3)
                    {
                        Month3++;
                    }
                }
                totalOrders += Month3;
                orders4 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1998-04-01") && x.OrderDate <= Convert.ToDateTime("1998-04-30")).ToList();
                foreach (var item in orders4)
                {
                    ordersd4 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                    foreach (var od in ordersd4)
                    {
                        Month4++;
                    }
                }
                totalOrders += Month4;
                orders5 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1998-05-01") && x.OrderDate <= Convert.ToDateTime("1998-05-30")).ToList();
                foreach (var item in orders5)
                {
                    ordersd5 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                    foreach (var od in ordersd5)
                    {
                        Month5++;
                    }
                }
                totalOrders += Month5;
                orders6 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1998-06-01") && x.OrderDate <= Convert.ToDateTime("1998-06-30")).ToList();
                foreach (var item in orders6)
                {
                    ordersd6 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                    foreach (var od in ordersd6)
                    {
                        Month6++;
                    }
                }
                totalOrders += Month6;
                orders7 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1998-07-01") && x.OrderDate <= Convert.ToDateTime("1998-07-30")).ToList();
                foreach (var item in orders7)
                {
                    ordersd7 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                    foreach (var od in ordersd7)
                    {
                        Month7++;
                    }
                }
                totalOrders += Month7;
                orders8 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1998-08-01") && x.OrderDate <= Convert.ToDateTime("1998-08-30")).ToList();
                foreach (var item in orders8)
                {
                    ordersd8 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                    foreach (var od in ordersd8)
                    {
                        Month8++;
                    }
                }
                totalOrders += Month8;
                orders9 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1998-09-01") && x.OrderDate <= Convert.ToDateTime("1998-09-30")).ToList();
                foreach (var item in orders9)
                {
                    ordersd9 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                    foreach (var od in ordersd9)
                    {
                        Month9++;
                    }
                }
                totalOrders += Month9;
                orders10 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1998-10-01") && x.OrderDate <= Convert.ToDateTime("1998-10-30")).ToList();
                foreach (var item in orders10)
                {
                    ordersd10 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                    foreach (var od in ordersd10)
                    {
                        Month10++;
                    }
                }
                totalOrders += Month10;
                orders11 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1998-11-01") && x.OrderDate <= Convert.ToDateTime("1998-11-30")).ToList();
                foreach (var item in orders11)
                {
                    ordersd11 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                    foreach (var od in ordersd11)
                    {
                        Month11++;
                    }
                }
                totalOrders += Month11;
                orders12 = _dbContext.Orders.Where(x => x.OrderDate >= Convert.ToDateTime("1998-12-01") && x.OrderDate <= Convert.ToDateTime("1998-12-30")).ToList();
                foreach (var item in orders12)
                {
                    ordersd12 = _dbContext.OrderDetails.Where(o => o.OrderId == item.OrderId).ToList();
                    foreach (var od in ordersd12)
                    {
                        Month12++;
                    }
                }
                totalOrders += Month12;
            }
        }
    }
}

