namespace BullsAndCows.Data.Contracts
{
    using System;
    using System.Linq;

    public interface IRepository<T>
    {
        IQueryable<T> All();

        T GetById(object id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void DeleteById(object id);

        int SaveChanges();
    }
}
