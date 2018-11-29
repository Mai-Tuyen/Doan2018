using DigitalShop.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalShop.Service.IRepository
{
    public interface ICategoryRepository
    {
        Category GetById(int id);
        List<Category> GetListCategory();
        void Add(Category category);
        void Save();
        void Deactivate(int id);
    }   
}
