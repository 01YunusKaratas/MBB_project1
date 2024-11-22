using MBB_project1.Models;
using Microsoft.EntityFrameworkCore;

namespace MBB_project1.Data
{
    public class ApplicationDbContext:DbContext //entity framework ile ilgil
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
    {
    }


        public DbSet<Product> product_table{ get; set;}    
        public DbSet<User> user_table { get; set; } 
    }
}
