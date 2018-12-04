using DigitalShop.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalShop.Service.IRepository
{
    public interface IAdminRepository
    {
        Admin GetById(int id);
        Admin GetByUserName(string UserName);
        List<Admin> GetListAdmin();
        void Add(Admin admin);
        void Save();
        void Deactivate(int id);

        bool CheckAdminLogin(string userName, string passWord);
    }
}
