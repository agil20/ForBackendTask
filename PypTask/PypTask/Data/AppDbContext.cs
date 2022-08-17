using Microsoft.EntityFrameworkCore;
using PypTask.Models;

namespace PypTask.Data
{
    public class AppDbContext:DbContext
    {
      public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
      {

      }
        public DbSet<ExcelUpload> ExcelUploads { get; set; }

    }
}
