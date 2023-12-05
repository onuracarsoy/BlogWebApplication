using Microsoft.EntityFrameworkCore;

namespace BlogApi.DataAccessLayer
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=WINDEV2209EVAL; Database=CoreBlogApiDb; Integrated Security=True; TrustServerCertificate=True");
        }
        public DbSet<Employee> Employees { get; set; }

    }
}
