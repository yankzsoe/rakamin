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
        public DbSet<Pegawai> Pegawai { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder) {
            builder.UseNpgsql(_Configuration.GetConnectionString("postgresdb"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new PegawaiConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
