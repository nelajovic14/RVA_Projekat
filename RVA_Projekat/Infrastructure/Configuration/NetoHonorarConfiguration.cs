using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RVA_Projekat.Model;

namespace RVA_Projekat.Infrastructure.Configuration
{
    public class NetoHonorarConfiguration : IEntityTypeConfiguration<NetoHonorar>
    {
        public void Configure(EntityTypeBuilder<NetoHonorar> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
