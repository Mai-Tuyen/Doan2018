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

        public void Delete(int id)
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
            return context.Category.Where(x => x.Status == true).ToList();
        }

        public void Update(int id)
        {
            var category = context.Category.Find(id);
            context.Category.Update(category);
            context.SaveChanges();
        }
    }
}
