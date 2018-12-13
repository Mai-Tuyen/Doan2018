using DigitalShop.Entity;
using DigitalShop.Service;
using DigitalShop.Service.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitialShop.Service.Repository
{
    
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DigitalDBContext context;
        public CustomerRepository(DigitalDBContext context)
        {
            this.context = context;
        }
        public void Deactivate(int id)
        {
            var customer = context.Customer.Find(id);
            customer.Status = false;
            context.SaveChanges();
        }
        public void Activate(int id)
        {
            var customer = context.Customer.Find(id);
            customer.Status = true;
            context.SaveChanges();
        }

        public Customer GetById(int id)
        {
            return context.Customer.Find(id);
        }

        public List<Customer> GetListCustomer()
        {
            return context.Customer.ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }
        public void Add(Customer newCustomer)
        {
            context.Customer.Add(newCustomer);
            context.SaveChanges();
        }
    }
}
