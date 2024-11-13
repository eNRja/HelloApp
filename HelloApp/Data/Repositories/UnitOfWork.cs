public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IRepository<User>? _userRepository;  // Сделать nullable
    // Если необходимо: private IRepository<Order>? _orderRepository;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IRepository<User> UserRepository
    {
        get { return _userRepository ??= new Repository<User>(_context); }
    }

    // public IRepository<Order> OrderRepository
    // {
    //     get { return _orderRepository ??= new Repository<Order>(_context); }
    // }

    public void Commit()
    {
        _context.SaveChanges();
        //_context.CommitTransaction();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Rollback()
    {
        //_context.RollbackTransaction();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
