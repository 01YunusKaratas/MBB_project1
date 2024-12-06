using MBB_project1.Models;
using Microsoft.EntityFrameworkCore;

namespace MBB_project1.Data
{
    public class ApplicationDbContext:DbContext //entity framework ile ilgil
    {
        public ApplicationDbContext() //Constructor
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
    {
    }
        //dataya bağlanmak için 

        public DbSet<Product> product_table{ get; set;} //Bu kodu Product_Tablela bağlantı sağlamak için yazdık
        public DbSet<User> user_table { get; set; }  //Bu koduda User_table sınıfıyla bağlantı sağlamak için yaptık
    }
}


