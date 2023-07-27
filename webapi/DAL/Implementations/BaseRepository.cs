using DAL.DbContexts;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementations
{
    public abstract class BaseRepository<TDomainClass> : IBaseRepository<TDomainClass, int>
       where TDomainClass : class
    {
        protected abstract DbSet<TDomainClass> DbSet { get; }
        private readonly DefaultContext _context;
        public BaseRepository(DefaultContext context)
        {
            _context = context;
        }

        public void Create(TDomainClass instance)
        {
            DbSet.Add(instance);
        }

        public void CreateRange(IEnumerable<TDomainClass> instances)
        {
            DbSet.AddRange(instances);
        }

        public void Update(TDomainClass instance)
        {
            DbSet.Update(instance);
        }

        public void UpdateRange(IEnumerable<TDomainClass> instances)
        {
            DbSet.UpdateRange(instances);
        }

        public void Delete(TDomainClass instance)
        {
            DbSet.Remove(instance);
        }

        public void DeleteRange(IEnumerable<TDomainClass> instances)
        {
            DbSet.RemoveRange(instances);
        }

        public TDomainClass GetByPrimaryKey(int id)
        {
            return DbSet.Find(id);
        }

        public async Task<TDomainClass> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public IQueryable<TDomainClass> GetAll()
        {
            return DbSet;
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}