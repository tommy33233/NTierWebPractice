using System;
using System.Collections.Generic;
using System.Linq;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using System.Data.Entity;

namespace DAL.Repositories
{
    class PhoneRepository : IRepository<Phone>
    {
        private MobileContext db;

        public PhoneRepository(MobileContext context)
        {
            this.db = context;
        }

        //public PhoneRepository()
        //{
        //    db = new MobileContext();
        //}

        public void Create(Phone phone)
        {
            db.Phones.Add(phone);
        }

        public void Delete(int id)
        {
            Phone phone = db.Phones.Find(id);
            if (phone != null)
                db.Phones.Remove(phone);
        }

        public IEnumerable<Phone> Find(Func<Phone, bool> predicate)
        {
            return db.Phones.Where(predicate).ToList();
        }

        public Phone Get(int id)
        {
            return db.Phones.Find(id);
        }

        public IEnumerable<Phone> GetAll()
        {
            return db.Phones;
        }

        public void Update(Phone item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
