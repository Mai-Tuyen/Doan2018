using DigitalShop.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalShop.Service.IRepository
{
    public interface IProductRepository
    {
        Product GetById(int id);
        List<Product> GetListProduct();
        void Add(Product product);
        void Save();
        void Deactive(int id);
        List<string> GetListImage(int id);
    }
}
