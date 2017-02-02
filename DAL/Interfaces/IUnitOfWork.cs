using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Phone> Phones { get; }
        IRepository<Order> Orders { get; }

        void Save();
        void Dispose();
    }
}