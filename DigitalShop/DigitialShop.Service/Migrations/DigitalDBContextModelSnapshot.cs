﻿// <auto-generated />
using System;
using DigitalShop.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DigitialShop.Service.Migrations
{
    [DbContext(typeof(DigitalDBContext))]
    partial class DigitalDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DigitalShop.Entity.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PassWord");

                    b.Property<bool>("Status");

                    b.Property<string>("Type");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("DigitalShop.Entity.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<bool>("Status");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("DigitalShop.Entity.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("ntext");

                    b.Property<string>("DisplayName");

                    b.Property<string>("PassWord");

                    b.Property<string>("Phone");

                    b.Property<bool>("Status");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("DigitalShop.Entity.ImagesDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImageDetail");

                    b.Property<int>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ImagesDetail");
                });

            modelBuilder.Entity("DigitalShop.Entity.Import", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<DateTime>("CreateAt");

                    b.Property<int>("CreateBy");

                    b.Property<string>("Detail")
                        .HasColumnType("ntext");

                    b.HasKey("Id");

                    b.HasIndex("CreateBy");

                    b.ToTable("Import");
                });

            modelBuilder.Entity("DigitalShop.Entity.ImportDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ImportId");

                    b.Property<int>("ProductId");

                    b.Property<int>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("ImportId");

                    b.HasIndex("ProductId");

                    b.ToTable("ImportDetail");
                });

            modelBuilder.Entity("DigitalShop.Entity.Manufacturer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<bool>("Status");

                    b.HasKey("Id");

                    b.ToTable("Manufacturer");
                });

            modelBuilder.Entity("DigitalShop.Entity.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<DateTime>("CreateAt");

                    b.Property<int>("CustomerId");

                    b.Property<string>("ShipAddress");

                    b.Property<string>("ShipMobile");

                    b.Property<string>("ShipName");

                    b.Property<string>("Status");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("DigitalShop.Entity.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OrderId");

                    b.Property<int>("ProductId");

                    b.Property<int>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetail");
                });

            modelBuilder.Entity("DigitalShop.Entity.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId");

                    b.Property<DateTime>("CreateAt");

                    b.Property<int>("CreateBy");

                    b.Property<string>("Description");

                    b.Property<string>("Image");

                    b.Property<int>("ManufacturerId");

                    b.Property<string>("Name");

                    b.Property<double>("PriceIn");

                    b.Property<double>("PriceOut");

                    b.Property<int>("Quantity");

                    b.Property<bool>("Status");

                    b.Property<int>("ViewCount");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CreateBy");

                    b.HasIndex("ManufacturerId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("DigitalShop.Entity.ImagesDetail", b =>
                {
                    b.HasOne("DigitalShop.Entity.Product", "Product")
                        .WithMany("ImagesDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DigitalShop.Entity.Import", b =>
                {
                    b.HasOne("DigitalShop.Entity.Admin", "Admin")
                        .WithMany("Imports")
                        .HasForeignKey("CreateBy")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DigitalShop.Entity.ImportDetail", b =>
                {
                    b.HasOne("DigitalShop.Entity.Import", "Import")
                        .WithMany("ImportDetails")
                        .HasForeignKey("ImportId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DigitalShop.Entity.Product", "Product")
                        .WithMany("ImportDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DigitalShop.Entity.Order", b =>
                {
                    b.HasOne("DigitalShop.Entity.Customer", "Customer")
                        .WithMany("Order")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DigitalShop.Entity.OrderDetail", b =>
                {
                    b.HasOne("DigitalShop.Entity.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DigitalShop.Entity.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DigitalShop.Entity.Product", b =>
                {
                    b.HasOne("DigitalShop.Entity.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DigitalShop.Entity.Admin", "Admin")
                        .WithMany("Products")
                        .HasForeignKey("CreateBy")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DigitalShop.Entity.Manufacturer", "Manufacturer")
                        .WithMany("Products")
                        .HasForeignKey("ManufacturerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
