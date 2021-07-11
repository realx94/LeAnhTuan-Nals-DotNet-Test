using System.Linq;

namespace Core.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();

        T Add(T entity);

        T Update(T entity);

        bool Delete(T entity);

        int SaveChanges();
    }
}
