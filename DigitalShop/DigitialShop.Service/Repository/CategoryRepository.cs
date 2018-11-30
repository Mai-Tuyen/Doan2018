using DigitalShop.Entity;
using DigitalShop.Service;
using DigitalShop.Service.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace DigitialShop.Service.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DigitalDBContext context;
        public CategoryRepository(DigitalDBContext context)
        {
            this.context = context;
        }

        public void Add(Category category)
        {
            context.Category.Add(category);
            context.SaveChanges();
        }

        public void Deactivate(int id)
        {
            var category = context.Category.Find(id);
            category.Status = false;
            context.SaveChanges();
        }

        public Category GetById(int id)
        {
            return context.Category.Find(id);
        }

        public List<Category> GetListCategory()
        {
            return context.Category.ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
