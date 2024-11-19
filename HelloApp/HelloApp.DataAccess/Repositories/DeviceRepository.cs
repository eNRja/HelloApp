using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HelloApp.DataAccess.Repositories
{
    public class DeviceRepository : IRepository<Device>
    {
        private readonly DbAppContext _context;

        public DeviceRepository(DbAppContext context)
        {
            _context = context;
        }

        public async Task<Device?> GetByIdAsync(int id) => await _context.Devices.FindAsync(id);

        public async Task<List<Device>> GetAllAsync() => await _context.Devices.ToListAsync();

        public async Task AddAsync(Device device) => await _context.Devices.AddAsync(device);

        public async Task<Device?> FindAsync(Expression<Func<Device, bool>> predicate)
        {
            return await _context.Devices.FirstOrDefaultAsync(predicate);
        }

        public void Remove(Device device) => _context.Devices.Remove(device);

        public void Update(Device device) => _context.Devices.Update(device);
    }
}
