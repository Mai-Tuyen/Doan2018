using DigitalShop.Entity;
using DigitalShop.Service;
using DigitalShop.Service.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitialShop.Service.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DigitalDBContext context;
        public OrderRepository(DigitalDBContext context)
        {
            this.context = context;
        }
        public void Add(Order order)
        {
            context.Order.Add(order);
            context.SaveChanges();
        }

        public void AddOrderDetail(OrderDetail orderDetail)
        {
            context.OrderDetail.Add(orderDetail);
            context.SaveChanges();
        }

        public List<OrderDetail> GetAllOrderDetail()
        {
            return context.OrderDetail.Include(p => p.Product)
                 .Include(q => q.Order)
                 .OrderBy(x => x.Quantity)
                 .ToList();
        }

        public Order GetById(int id)
        {
            return context.Order.Find(id);
        }

        public List<Order> GetListOrder()
        {
            return context.Order.Include(p => p.Customer)
                 .OrderBy(x => x.CreateAt)
                 .ToList();
        }

        public List<OrderDetail> GetListOrderDetail(int id)
        {
            return context.OrderDetail.Include(p => p.Product)
                .Include(q => q.Order)
                .Where(x => x.OrderId == id)
                .OrderBy(x => x.Quantity)
                .ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
