using DigitalShop.Entity;
using DigitalShop.Service;
using DigitalShop.Service.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitialShop.Service.Repository
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly DigitalDBContext context;
        public ManufacturerRepository(DigitalDBContext context)
        {
            this.context = context;
        }

        public void Add(Manufacturer manufacturer)
        {
            context.Manufacturer.Add(manufacturer);
            context.SaveChanges();
        }

        public void Deactive(int id)
        {
            var category = context.Manufacturer.Find(id);
            category.Status = false;
            context.SaveChanges();
        }

        public Manufacturer GetById(int id)
        {
            return context.Manufacturer.Find(id);
        }

        public List<Manufacturer> GetListManufacture()
        {
            return context.Manufacturer.ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
