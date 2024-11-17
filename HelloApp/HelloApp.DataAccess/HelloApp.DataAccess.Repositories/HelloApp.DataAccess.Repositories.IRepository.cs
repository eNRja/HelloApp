using System.Linq.Expressions;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync();
    Task<T?> FindAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    void Remove(T entity);
    void Update(T entity);
}
