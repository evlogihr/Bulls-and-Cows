namespace BullsAndCows.Data.Contracts
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using BullsAndCows.Data.Models;

    public interface IBullsAndCowsDbContext : IDisposable
    {
        IDbSet<Game> Games { get; set;}

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entry) where TEntity : class;

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        int SaveChanges();
    }
}
