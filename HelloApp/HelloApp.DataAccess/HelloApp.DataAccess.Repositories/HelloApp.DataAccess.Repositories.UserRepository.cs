using HelloApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HelloApp.Data.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly DbAppContext _context;

        public UserRepository(DbAppContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(int id) => await _context.Users.FindAsync(id);

        public async Task<List<User>> GetAllAsync() => await _context.Users.ToListAsync();

        public async Task AddAsync(User user) => await _context.Users.AddAsync(user);

        public async Task<User?> FindAsync(Expression<Func<User, bool>> predicate)
        {
            return await _context.Users.FirstOrDefaultAsync(predicate);
        }

        public void Remove(User user) => _context.Users.Remove(user);

        public void Update(User user) => _context.Users.Update(user);



    }
}
