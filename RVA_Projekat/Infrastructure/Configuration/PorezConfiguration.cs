using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RVA_Projekat.Model;

namespace RVA_Projekat.Infrastructure.Configuration
{
    public class PorezConfiguration : IEntityTypeConfiguration<Porez>
    {
        public void Configure(EntityTypeBuilder<Porez> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
