using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiExample.Model;

namespace WebApiExample.DataContext.Configrurations {
    public class PegawaiConfiguration : IEntityTypeConfiguration<Pegawai> {
        public void Configure(EntityTypeBuilder<Pegawai> builder) {
            // Set PrimaryKey
            builder.HasKey(x => x.Id);
        }
    }
}
