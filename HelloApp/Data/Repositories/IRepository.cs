using Microsoft.EntityFrameworkCore;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync();
    Task AddAsync(T entity);
    void Remove(T entity);
    void Update(T entity);
}


public class UserRepository : IRepository<User>
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(int id) => await _context.Users.FindAsync(id);

    public async Task<List<User>> GetAllAsync() => await _context.Users.ToListAsync();

    public async Task AddAsync(User user) => await _context.Users.AddAsync(user);

    public void Remove(User user) => _context.Users.Remove(user);

    public void Update(User user) => _context.Users.Update(user);
}
