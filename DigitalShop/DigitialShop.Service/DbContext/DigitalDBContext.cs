using DigitalShop.Entity;
using Microsoft.EntityFrameworkCore;

namespace DigitalShop.Service
{
    public class DigitalDBContext : DbContext
    {

        public DbSet<Admin> Admin { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ImagesDetail> ImagesDetail { get; set; }
        public DbSet<Import> Import { get; set; }
        public DbSet<ImportDetail> ImportDetail { get; set; }
        public DbSet<Manufacturer> Manufacturer { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
      

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .EnableSensitiveDataLogging(true)
                .UseSqlServer(@"Data Source=USERMIC-8EMT522\SQLEXPRESS;Initial Catalog=DigitalShop; Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ImportDetail>()
                .HasOne(x => x.Product)
                .WithMany(y => y.ImportDetails)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
