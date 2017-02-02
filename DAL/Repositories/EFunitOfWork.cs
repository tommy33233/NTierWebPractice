using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class EFunitOfWork : IUnitOfWork
    {
        private MobileContext db;
        private PhoneRepository phoneRepository;
        private OrderRepository orderRepository;

        public EFunitOfWork(string connection)
        {
            db = new MobileContext(connection);
        }
        //public EFunitOfWork()
        //{
        //    db=new MobileContext();
        //}

        public IRepository<Phone> Phones
        {
            get
            {
                if (phoneRepository == null)
                    phoneRepository = new PhoneRepository(db);
                //   phoneRepository=new PhoneRepository();


                return phoneRepository;
            }
        }

        public IRepository<Order> Orders
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new OrderRepository(db);
                // orderRepository=new OrderRepository();

                return orderRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
