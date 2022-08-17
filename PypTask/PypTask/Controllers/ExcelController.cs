using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using PypTask.Data;
using PypTask.Helper;
using PypTask.Models;
using System;
using System.IO;
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
        //UploadData/POST/File(binary) - məlumat faylı yüklənməsi - fayl yalnız
        //xlxs və xls ola bilər, max 5mb yükləyə bilməlidir, yüklənmiş faylın template uyğun
        //olması lazımdır.
        [HttpPost]
        public async Task< IActionResult> UploadData(IFormFile file)
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
            var token = "";
            string subject = "Endirim var!";
            EmailHelper helper = new EmailHelper(_config.GetSection("ConfirmationParam:Email").Value, _config.GetSection("ConfirmationParam:Password").Value);

            token = $"Salam";
            var emailResult = helper.SendNews("aqilsahib3@gmail.com", token, subject);


            string confirmation = Url.Action("ConfirmEmail", "Account", new
            {
                token
            }, Request.Scheme);
            return StatusCode(201, "Created");

        }
      
             }
}
