using System;

namespace PypTask.Models
{
    public class ExcelUpload
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string Segment { get; set; }
        public string Product { get; set; }
        public string DisCountBand { get; set; }
        public double UnitsSold { get; set; }
        public double ManuFactor { get; set; }
        public double SalePrice { get; set; }
        public double GrossSales { get; set; }
        public double DisCounts { get; set; }
        public double Sales { get; set; }
        public double Cogs { get; set; }
        public double Profit { get; set; }
        public DateTime Date { get; set; }
    }
}
