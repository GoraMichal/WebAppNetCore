using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebAppNetCore.Models
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly DataContext context;

        public OrdersRepository(DataContext _context) => context = _context;

        public IEnumerable<Order> Orders => context.Orders
            .Include(o => o.Lines).ThenInclude(l => l.Product);

        public Order GetOrder(long key) => context.Orders
            .Include(o => o.Lines).First(o => o.Id == key);
        
        public void AddOrder(Order order)
        {
            context.Orders.Add(order);
            context.SaveChanges();
        }

        public void UpdateOrder(Order order)
        {
            context.Orders.Update(order);
            context.SaveChanges();
        }

        public void DeleteOrder(Order order)
        {
            context.Orders.Remove(order);
            context.SaveChanges();
        }
    }
}
