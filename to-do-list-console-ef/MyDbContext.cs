using to_do_list_console_ef.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace to_do_list_console_ef
{
    public class MyDbContext : DbContext
    {
        // add tables
        public DbSet<Duty> Duty { get; set; } // tasks table
        public DbSet<Status> Status { get; set; } // statuses table

        // configure database
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // can use UseInMemoryDatabase for testing
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=projects_EF;Encrypt=false;Trusted_Connection=True"); 
        }
    }
}
