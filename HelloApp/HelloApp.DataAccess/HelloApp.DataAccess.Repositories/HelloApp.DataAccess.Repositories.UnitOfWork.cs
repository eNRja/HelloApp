using HelloApp.Models;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbAppContext _context;
    private IRepository<User>? _userRepository;
    private IRepository<Device>? _deviceRepository;

    public UnitOfWork(DbAppContext context)
    {
        _context = context;
    }

    public IRepository<User> UserRepository
    {
        get { return _userRepository ??= new Repository<User>(_context); }
    }

    public IRepository<Device> DeviceRepository
    {
        get { return _deviceRepository ??= new Repository<Device>(_context); }
    }

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
