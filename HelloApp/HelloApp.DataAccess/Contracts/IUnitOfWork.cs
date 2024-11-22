using HelloApp.DataAccess;

public interface IUnitOfWork : IDisposable
{
    IRepository<User> UserRepository { get; }
    IRepository<Device> DeviceRepository { get; }
    Task SaveChangesAsync();
    void Commit();
    void Rollback();
}
