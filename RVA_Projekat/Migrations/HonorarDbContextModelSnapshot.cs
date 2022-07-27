﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RVA_Projekat.Infrastructure;

#nullable disable

namespace RVA_Projekat.Migrations
{
    [DbContext(typeof(HonorarDbContext))]
    partial class HonorarDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("RVA_Projekat.Model.BrutoHonorar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("TrenutnaPlata")
                        .HasColumnType("int");

                    b.Property<int>("valuta")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("BrutoHonorars");
                });

            modelBuilder.Entity("RVA_Projekat.Model.NetoHonorar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BrutoHonorarId")
                        .HasColumnType("int");

                    b.Property<int>("IdPorez")
                        .HasColumnType("int");

                    b.Property<int>("umanjenje")
                        .HasColumnType("int");

                    b.Property<int>("uvecanje")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BrutoHonorarId");

                    b.ToTable("NetoHonorars");
                });

            modelBuilder.Entity("RVA_Projekat.Model.Porez", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("NetoHonorarId")
                        .HasColumnType("int");

                    b.Property<int>("Tip")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NetoHonorarId");

                    b.ToTable("Porez");
                });

            modelBuilder.Entity("RVA_Projekat.Model.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Password")
                        .HasColumnType("navarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("navarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RVA_Projekat.Model.Zaposleni", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BrutoHonorarId")
                        .HasColumnType("int");

                    b.Property<int>("GodineIskustva")
                        .HasColumnType("int");

                    b.Property<string>("Ime")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("BrutoHonorarId");

                    b.ToTable("Zaposlenis");
                });

            modelBuilder.Entity("RVA_Projekat.Model.NetoHonorar", b =>
                {
                    b.HasOne("RVA_Projekat.Model.BrutoHonorar", "honorar")
                        .WithMany()
                        .HasForeignKey("BrutoHonorarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("honorar");
                });

            modelBuilder.Entity("RVA_Projekat.Model.Porez", b =>
                {
                    b.HasOne("RVA_Projekat.Model.NetoHonorar", null)
                        .WithMany("Porezi")
                        .HasForeignKey("NetoHonorarId");
                });

            modelBuilder.Entity("RVA_Projekat.Model.Zaposleni", b =>
                {
                    b.HasOne("RVA_Projekat.Model.BrutoHonorar", "honorar")
                        .WithMany()
                        .HasForeignKey("BrutoHonorarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("honorar");
                });

            modelBuilder.Entity("RVA_Projekat.Model.NetoHonorar", b =>
                {
                    b.Navigation("Porezi");
                });
#pragma warning restore 612, 618
        }
    }
}
