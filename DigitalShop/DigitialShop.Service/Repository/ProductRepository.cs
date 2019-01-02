using DigitalShop.Entity;
using DigitalShop.Service;
using DigitalShop.Service.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DigitialShop.Service.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DigitalDBContext context;
        public ProductRepository(DigitalDBContext context)
        {
            this.context = context;
        }
        public void Add(Product product)
        {
            context.Product.Add(product);
            context.SaveChanges();
        }

        public void Deactive(int id)
        {
            var product = context.Product.Find(id);
            product.Status = false;
            context.SaveChanges();
        }

        public Product GetById(int id)
        {
            var product = context.Product.Find(id);
            return product;
        }

        public List<string> GetListImage(int id)
        {
            var product = context.Product.Find(id);
            return product.Image.Split('|').ToList();
        }

        public List<Product> GetListProduct()
        {
            return context.Product
                .Include(p=>p.Category)
                .Include(p=>p.Manufacturer)
                .Include(p=>p.Admin)
                .Where(x=>x.Category.Status==true && x.Manufacturer.Status==true)
                .ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
