using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HelloApp.DataAccess.Repositories
{
    public class UserRepository : Repository<DbUser>
    {
        public UserRepository(DbAppContext context) : base(context)
        {
        }
    }
}
