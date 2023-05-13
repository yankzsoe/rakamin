using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiExample.Model;

namespace WebApiExample.DataContext.Configrurations {
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department> {
        public void Configure(EntityTypeBuilder<Department> builder) {
            // Table Name
            builder.ToTable("Departments");

            // Set PrimaryKey
            builder.HasKey(x => x.Id);

            // Set Index
            builder.HasIndex(x => x.DepartmentName)
                .IsUnique();
        }
    }
}
