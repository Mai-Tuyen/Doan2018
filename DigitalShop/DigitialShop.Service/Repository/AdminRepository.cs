using DigitalShop.Entity;
using DigitalShop.Service;
using DigitalShop.Service.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitialShop.Service.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly DigitalDBContext context;
        public AdminRepository(DigitalDBContext context)
        {
            this.context = context;
        }
        public void Add(Admin admin)
        {
            context.Admin.Add(admin);
            context.SaveChanges();
        }

        public bool CheckAdminLogin(string userName, string passWord)
        {
            foreach (var item in context.Admin)
            {
                if (item.UserName == userName.Trim() && item.PassWord == passWord.Trim())
                {
                    return true;
                }
            }
            return false;
        }

        public void Deactivate(int id)
        {
            var admin = context.Admin.Find(id);
            admin.Status = false;
            context.SaveChanges();
        }

        public Admin GetById(int id)
        {
            var admin = context.Admin.Find(id);
            return admin;
        }
        public Admin GetByUserName(string userName)
        {
            var admin = context.Admin.Where(x=> x.UserName == userName).FirstOrDefault();
            return admin;
        }

        public List<Admin> GetListAdmin()
        {
            return context.Admin.OrderBy(x=>x.Status).ThenBy(x=>x.Type).ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
