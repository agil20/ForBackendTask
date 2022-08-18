using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using PypTask.Data;
using PypTask.Dtos;
using PypTask.Helper;

using PypTask.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PypTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelController : ControllerBase
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _env;
        private IConfiguration _config;

        public ExcelController(AppDbContext context, IWebHostEnvironment env = null, IConfiguration config = null)
        {

            _context = context;
            _env = env;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> UploadData(IFormFile file)
        {
            var extention = Path.GetExtension(file.Name);
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowcount = worksheet.Dimension.Rows;
                    for (int row = 2; row <= rowcount; row++)
                    {
                        ExcelUpload data = new ExcelUpload();
                        data.Segment = worksheet.Cells[row, 1].Value.ToString().Trim();
                        data.Country = worksheet.Cells[row, 2].Value.ToString().Trim();
                        data.Product = worksheet.Cells[row, 3].Value.ToString().Trim();
                        data.DisCountBand = worksheet.Cells[row, 4].Value.ToString().Trim();
                        data.ManuFactor = double.Parse(worksheet.Cells[row, 5].Value.ToString().Trim());
                        data.UnitsSold = double.Parse(worksheet.Cells[row, 6].Value.ToString().Trim());
                        data.DisCounts = double.Parse(worksheet.Cells[row, 7].Value.ToString().Trim());
                        data.SalePrice = double.Parse(worksheet.Cells[row, 8].Value.ToString().Trim());
                        data.GrossSales = double.Parse(worksheet.Cells[row, 9].Value.ToString().Trim());
                        data.Sales = double.Parse(worksheet.Cells[row, 10].Value.ToString().Trim());
                        data.Cogs = double.Parse(worksheet.Cells[row, 11].Value.ToString().Trim());
                        data.Profit = double.Parse(worksheet.Cells[row, 12].Value.ToString().Trim());
                        data.Date = DateTime.Parse(worksheet.Cells[row, 13].Value.ToString().Trim());
                        await _context.AddAsync(data);
                    }
                }




                await _context.SaveChangesAsync();
            }

            return StatusCode(201, "Created");

        }

        //SendReport/GET/Type(int) | StartDate(DateTime) | EndDate(DateTime) | AcceptorEmail(string[]) -
        //hesabatın növü və email ünvanları göndərərək hesabat istəyi - Type enum olmalıdır və seçimlərin
        //çölündə ola bilməz, emaillərin düzgün formatda bitiyini və code.edu.az domainə aid olduğunu yoxlamaq.
        //StartDate-in EndDate-dən kiçik olduğunu yoxlamaq.

        //[HttpGet]
        //public IActionResult SendRepo([FromQuery] SendFilterDto filter)
        //{
        //    var dataList = new List<ReturnDto>();
        //    var query = _context.ExcelUploads.Where(e => e.Date <= filter.EndDate && e.Date >= filter.StartDate);
        //    switch (filter.SendType)
        //    {
        //        case SendType.Segment:
        //            dataList = query.GroupBy(d => d.Segment).Select(data => new ReturnDto
        //            {
        //                Name = data.Key,
        //                Count = data.Key.Count(),
        //                TotalProfit = data.Sum(x => x.Profit),
        //                TotalDiscount = data.Sum(x => x.DisCounts),
        //                TotalSale = data.Sum(x => x.Sales),
        //            }).ToList();
        //            break;
        //        case SendType.Country:
        //            dataList = query.GroupBy(d => d.Country).Select(data => new ReturnDto
        //            {
        //                Name = data.Key,
        //                Count = data.Key.Count(),
        //                TotalProfit = data.Sum(x => x.Profit),
        //                TotalDiscount = data.Sum(x => x.DisCounts),
        //                TotalSale = data.Sum(x => x.Sales),
        //            }).ToList();
        //            break;
        //        case SendType.Product:
        //            dataList = query.GroupBy(d => d.Product).Select(data => new ReturnDto
        //            {
        //                Name = data.Key,
        //                Count = data.Key.Count(),
        //                TotalProfit = data.Sum(x => x.Profit),
        //                TotalDiscount = data.Sum(x => x.DisCounts),
        //                TotalSale = data.Sum(x => x.Sales),
        //            }).ToList();
        //            break;
        //        case SendType.Discounts:
        //            ReturnDto data = new ReturnDto();
        //            foreach (var item in query.OrderBy(p => p.Product).ToList())
        //            {
        //                data.Name = "";
        //                data.TotalDiscount = 1 - (item.SalePrice - item.DisCounts) / 100;
        //                //data.totalDiscounts = 100 * (item.Discounts / item.salePrice);
        //            }
        //            break;
        //        default:
        //            break;
        //    }
        //    string fileName = Guid.NewGuid().ToString() + ".xlsx";
        //    var pathFolder = Path.Combine(_env.WebRootPath, "Files/" + fileName);
        //    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        //    using (var package = new ExcelPackage())
        //    {
        //        var workSheet = package.Workbook.Worksheets.Add("Sheet1").Cells[1, 1].LoadFromCollection(dataList, true);
        //        package.SaveAs(pathFolder);

        //        MemoryStream ms = new MemoryStream();

        //        using (var file = new FileStream(pathFolder, FileMode.Open, FileAccess.Read))
        //        {
        //            var bytes = new byte[file.Length];
        //            file.Read(bytes, 0, (int)file.Length);
        //            ms.Write(bytes, 0, (int)file.Length);
        //            file.Close();
        //            _service.SendEmail(filter.AcceptorEmail, "Salam", "Your raport", fileName, bytes);
        //        }

        //        return Ok("Sent");
        //    }

    //}

        
    }
}
