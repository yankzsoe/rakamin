using Microsoft.EntityFrameworkCore;
using WebApiExample.Model;

namespace WebApiExample.Helper {
    public class DataContext : DbContext {
        protected readonly IConfiguration _Configuration;
        public DataContext(IConfiguration configuration) {
            _Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder) {
            builder.UseNpgsql(_Configuration.GetConnectionString("postgresdb"));
        }

        public DbSet<Department> Departments { get; set; }
    }
}
