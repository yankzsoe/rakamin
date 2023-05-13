using Microsoft.EntityFrameworkCore;
using WebApiExample.DataContext.Configrurations;
using WebApiExample.Model;

namespace WebApiExample.DataContext {
    public class AppDataContext : DbContext {
        protected readonly IConfiguration _Configuration;
        public AppDataContext(IConfiguration configuration) {
            _Configuration = configuration;
        }

        public DbSet<Department> Departments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder) {
            builder.UseNpgsql(_Configuration.GetConnectionString("postgresdb"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new PegawaiConfiguration());

            // Ignore Employee Model
            // This model used by dummy.restapiexample
            modelBuilder.Ignore<Employee>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
