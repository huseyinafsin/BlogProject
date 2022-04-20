using Microsoft.EntityFrameworkCore;

namespace BlogApiDemo.DataAccessLayer
{
    public class ApiContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-KRNCUB4\\SQLEXPRESS;Database=CoreBlogApiDb; Integrated Security=True;");
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
