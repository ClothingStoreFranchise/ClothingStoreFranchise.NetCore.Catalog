﻿// <auto-generated />
using System;
using ClothingStoreFranchise.NetCore.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ClothingStoreFranchise.NetCore.Catalog.Migrations.CatalogMigrations
{
    [DbContext(typeof(CatalogContext))]
    [Migration("20201011112956_Initial2")]
    partial class Initial2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ClothingStoreFranchise.NetCore.Catalog.Model.CatalogProduct", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CatalogProductId")
                        .HasColumnType("bigint");

                    b.Property<long?>("CurrentOfferId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PictureUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<long>("SubcategoryId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CurrentOfferId");

                    b.HasIndex("SubcategoryId");

                    b.ToTable("CatalogProducts");
                });

            modelBuilder.Entity("ClothingStoreFranchise.NetCore.Catalog.Model.Category", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 0)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CategoryBelongingId")
                        .HasColumnType("bigint");

                    b.Property<long?>("CurrentOfferId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TypeClothingSize")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryBelongingId");

                    b.HasIndex("CurrentOfferId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ClothingStoreFranchise.NetCore.Catalog.Model.Offer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CatalogProductId")
                        .HasColumnType("bigint");

                    b.Property<long?>("CategoryId")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CatalogProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("ClothingStoreFranchise.NetCore.Catalog.Model.CatalogProduct", b =>
                {
                    b.HasOne("ClothingStoreFranchise.NetCore.Catalog.Model.Offer", "CurrentOffer")
                        .WithMany()
                        .HasForeignKey("CurrentOfferId");

                    b.HasOne("ClothingStoreFranchise.NetCore.Catalog.Model.Category", "Subcategory")
                        .WithMany("CatalogProducts")
                        .HasForeignKey("SubcategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ClothingStoreFranchise.NetCore.Catalog.Model.Category", b =>
                {
                    b.HasOne("ClothingStoreFranchise.NetCore.Catalog.Model.Category", "CategoryBelonging")
                        .WithMany("Subcategories")
                        .HasForeignKey("CategoryBelongingId");

                    b.HasOne("ClothingStoreFranchise.NetCore.Catalog.Model.Offer", "CurrentOffer")
                        .WithMany()
                        .HasForeignKey("CurrentOfferId");
                });

            modelBuilder.Entity("ClothingStoreFranchise.NetCore.Catalog.Model.Offer", b =>
                {
                    b.HasOne("ClothingStoreFranchise.NetCore.Catalog.Model.CatalogProduct", null)
                        .WithMany("Offers")
                        .HasForeignKey("CatalogProductId");

                    b.HasOne("ClothingStoreFranchise.NetCore.Catalog.Model.Category", null)
                        .WithMany("Offers")
                        .HasForeignKey("CategoryId");
                });
#pragma warning restore 612, 618
        }
    }
}
