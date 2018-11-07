using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalShop.Model.Entity
{
    public class DigitalDBContext :DbContext
    {
       
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<ImagesDetail> ImagesDetail { get; set; }
        public DbSet<Import> Import { get; set; }
        public DbSet<ImportDetail> ImportDetail { get; set; }
        public DbSet<Manufacturer> Manufacturer { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Product> Product { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .EnableSensitiveDataLogging(true)
                .UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=DigitalShop; Trusted_Connection=True;");
        }
    }
}
