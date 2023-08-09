﻿// <auto-generated />
using CBTD.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CBTD.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230809064521_ManufacturerModelAdded")]
    partial class ManufacturerModelAdded
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CBTD.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int")
                        .HasColumnName("display_order");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_category");

                    b.ToTable("category", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DisplayOrder = 1,
                            Name = "Non-Alcoholic Beverages"
                        },
                        new
                        {
                            Id = 2,
                            DisplayOrder = 2,
                            Name = "Wine"
                        },
                        new
                        {
                            Id = 3,
                            DisplayOrder = 3,
                            Name = "Snacks"
                        });
                });

            modelBuilder.Entity("CBTD.Models.Manufacturer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_manufacturer");

                    b.ToTable("manufacturer", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Manufacturer1"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Manufacturer2"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Manufacturer3"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
