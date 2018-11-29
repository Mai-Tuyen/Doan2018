using DigitalShop.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalShop.Service.IRepository
{
    public interface IManufacturerRepository
    {
        Manufacturer GetById(int id);
        List<Manufacturer> GetListManufacture();
        void Add(Manufacturer manufacturer);
        void Save();
        void Deactive(int id);
    }
}
