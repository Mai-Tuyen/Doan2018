using DigitalShop.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalShop.Service.IRepository
{
    public interface ICustomerRepository
    {
        Customer GetById(int id);
        List<Customer> GetListCustomer();
        void Save();
        void Deactivate(int id);
        void Activate(int id);
        void Add(Customer customer);


    }
}
