using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class BaseRepository<T>
        where T : class
    {
        AuthDbContext context;
        protected DbSet<T> Set { get; set; }
        public BaseRepository(AuthDbContext context)
        {
            this.context = context;
            Set = context.Set<T>();
        }

        public T Add(T entity)
        {
            var result = this.Set.Add(entity);
            return result.Entity;
        }

        public bool Delete(T entity)
        {
            var result = this.Set.Remove(entity);
            return true;
        }

        public IQueryable<T> GetAll()
        {
            return this.Set;
        }

        public T Update(T entity)
        {
            var result = this.Set.Attach(entity);
            return result.Entity;
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }
    }
}
