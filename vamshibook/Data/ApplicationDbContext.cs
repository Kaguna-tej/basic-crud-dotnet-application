using Microsoft.EntityFrameworkCore;
using vamshibook.Models;

namespace vamshibook.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
              
        }

        public DbSet<Category> Categories { get; set; }
    }
}
