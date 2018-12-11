using DigitalShop.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalShop.Service.IRepository
{
    public interface IOrderRepository
    {
        List<Order> GetListOrder();
        List<OrderDetail> GetListOrderDetail(int id);
        List<OrderDetail> GetAllOrderDetail();
        void Add(Order import);
        void AddOrderDetail(OrderDetail orderDetail);
        Order GetById(int id);
        void Save();
    }
}
