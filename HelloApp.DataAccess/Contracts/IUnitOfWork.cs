using HelloApp.DataAccess;

public interface IUnitOfWork : IDisposable
{
    IRepository<DbUser> UserRepository { get; }
    IRepository<DbDevice> DeviceRepository { get; }
    Task SaveChangesAsync();
    void Commit();
    void Rollback();
}
