using HelloApp.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbAppContext _context;

    public IRepository<DbUser> UserRepository { get; }
    public IRepository<DbDevice> DeviceRepository { get; }

    public UnitOfWork(DbAppContext context)
    {
        _context = context;

        UserRepository = new Repository<DbUser>(_context);
        DeviceRepository = new Repository<DbDevice>(_context);
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
