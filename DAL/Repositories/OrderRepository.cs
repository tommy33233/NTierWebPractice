using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using System.Data.Entity;

namespace DAL.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private MobileContext db;

        public OrderRepository(MobileContext context)
        {
            db = context;
        }
        //public OrderRepository()
        //{
        //    db = new MobileContext();
        //}

        public void Create(Order order)
        {
            db.Orders.Add(order);
        }

        public void Delete(int id)
        {
            Order order = db.Orders.Find(id);
            if (order != null)
                db.Orders.Remove(order);
        }

        public IEnumerable<Order> Find(Func<Order, bool> predicate)
        {
            return db.Orders.Where(predicate).ToList();
        }

        public Order Get(int id)
        {
            return db.Orders.Find(id);
        }

        public IEnumerable<Order> GetAll()
        {
            return db.Orders;
        }

        public void Update(Order order)
        {
            db.Entry(order).State = EntityState.Modified;
        }
    }
}
