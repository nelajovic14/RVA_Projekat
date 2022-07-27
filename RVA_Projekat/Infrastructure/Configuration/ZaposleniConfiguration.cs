﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RVA_Projekat.Model;

namespace RVA_Projekat.Infrastructure.Configuration
{
    public class ZaposleniConfiguration : IEntityTypeConfiguration<Zaposleni>
    {
        public void Configure(EntityTypeBuilder<Zaposleni> builder)
        {
            builder.HasKey(x=> x.Id);   
            builder.Property(x=> x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Ime).HasMaxLength(30);

        }
    }
}
