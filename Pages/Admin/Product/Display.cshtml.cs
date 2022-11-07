using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyRazorPage.Models;
using OfficeOpenXml;
using System.Data;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace MyRazorPage.Pages.Product
{
    [Authorize(Roles = "1")]
    public class IndexModel : PageModel
    {
        public int totalProducts { get; set; }

        private IHostingEnvironment _environment;

        public int pageNo { get; set; }

        public int pageSize { get; set; }

        public List<Models.Product> products { get; set; }
        public List<Category> category { get; set; }
        private readonly PRN221_DBContext _dbContext;

        [BindProperty]
        public IFormFile UploadedExcelFile { get; set; }

        [BindProperty]
        public String Message { get; set; }

        public IndexModel(IHostingEnvironment environment,PRN221_DBContext dbContext)
        {
            _environment = environment;
            _dbContext = dbContext;
        }
        public async Task<IActionResult> OnGet(int p = 1, int s = 5)
        {
            //int ID = (int)HttpContext.Session.GetInt32("Role");
            //if (ID == 2)
            //{
            //    return RedirectToPage("/Error");
            //}
            products = _dbContext.Products.Include(c => c.Category).OrderByDescending(x=>x.ProductId).Skip((p - 1) * s).Take(s).ToList();
                    category = _dbContext.Categories.ToList();
                    pageSize = s;
                    totalProducts = _dbContext.Products.ToList().Count();
                    pageNo = p;
            return Page();
        }
        public void OnPost(int p = 1, int s = 5)
        {
            String cate = Request.Form["Category"];
            String search = Request.Form["Search"];
            if (cate.Equals("all") && search.Equals(""))
            {
                products = _dbContext.Products.Include(c => c.Category).Skip((p - 1) * s).Take(s).ToList();
                totalProducts = _dbContext.Products.ToList().Count();
            }
            if (cate.Equals("all") && !search.Equals(""))
            {
                products = _dbContext.Products.Include(c => c.Category).Where(r => r.ProductName.Contains(search)).Skip((p - 1) * s).Take(s).ToList();
                totalProducts = _dbContext.Products.Where(r => r.ProductName.Contains(search)).ToList().Count();
            }
            if (!cate.Equals("all") && search.Equals(""))
            {
                products = _dbContext.Products.Include(c => c.Category).Where(C => C.CategoryId == Convert.ToInt32(cate)).Skip((p - 1) * s).Take(s).ToList();
                totalProducts = _dbContext.Products.Where(C => C.CategoryId == Convert.ToInt32(cate)).ToList().Count();
            }
            if (!cate.Equals("all") && !search.Equals(""))
            {
                products = _dbContext.Products.Include(c => c.Category).Where(C => C.CategoryId == Convert.ToInt32(cate) && C.ProductName.Contains(search)).Skip((p - 1) * s).Take(s).ToList();
                totalProducts = _dbContext.Products.Where(C => C.CategoryId == Convert.ToInt32(cate) && C.ProductName.Contains(search)).ToList().Count();
            }
            category = _dbContext.Categories.ToList();
            pageSize = s;
            pageNo = p;
        }

        public async Task<IActionResult> OnPostUpload()
        {
            return await Import(UploadedExcelFile);

        }
        public async Task<IActionResult> Import(IFormFile formFile)
        {
            if (formFile == null || formFile.Length <= 0)
            {
                Message = "This is not a valid file.";
                return Page();
            }

            if (formFile.Length > 500000)
            {
                Message = "File should be less then 0.5 Mb";
                return Page();
            }

            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                Message = "Wrong file format. Should be xlsx.";
                return Page();
            }

            var newList = new List<Models.Product>();

            try
            {
                using (var stream = new MemoryStream())
                {
                    await formFile.CopyToAsync(stream);

                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            newList.Add(new Models.Product
                            {
                                //ID = row - 1,
                                ProductName = (worksheet.Cells[row, 1].Value.ToString().Trim()),
                                CategoryId = int.Parse(worksheet.Cells[row, 2].Value.ToString().Trim()),
                                QuantityPerUnit = (worksheet.Cells[row, 3].Value.ToString().Trim()),
                                UnitPrice = decimal.Parse(worksheet.Cells[row, 4].Value.ToString().Trim()),
                                UnitsInStock = short.Parse(worksheet.Cells[row, 5].Value.ToString().Trim()),
                                UnitsOnOrder = short.Parse(worksheet.Cells[row, 6].Value.ToString().Trim()),
                                ReorderLevel = short.Parse(worksheet.Cells[row, 7].Value.ToString().Trim()),
                                Discontinued = true
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                category = _dbContext.Categories.ToList();
                Message = "Error while parsing the file. Check the column order and format.";
                return Page();
            }


            List<Models.Product> oldInvoiceList = _dbContext.Products.ToList();
            //_dbContext.Products.RemoveRange(oldInvoiceList);
            _dbContext.Products.AddRange(newList);
            _dbContext.SaveChanges();
            //oldInvoiceList = _context.InvoiceTable.ToList();

            return RedirectToPage("Display");
        }
    }
}
