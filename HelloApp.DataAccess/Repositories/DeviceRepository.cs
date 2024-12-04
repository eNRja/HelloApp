using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HelloApp.DataAccess.Repositories
{
    public class DeviceRepository : Repository<DbDevice>
    {
        public DeviceRepository(DbAppContext context) : base(context)
        {
        }
    }
}
