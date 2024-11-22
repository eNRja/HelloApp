using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HelloApp.DataAccess.Repositories
{
    public class WeatherRepository : IRepository<Weather>
    {
        private readonly DbAppContext _context;

        public WeatherRepository(DbAppContext context)
        {
            _context = context;
        }

        public async Task<Weather?> GetByIdAsync(int id) => await _context.Weathers.FindAsync(id);

        public async Task<List<Weather>> GetAllAsync() => await _context.Weathers.ToListAsync();

        public async Task AddAsync(Weather weather) => await _context.Weathers.AddAsync(weather);

        public async Task<Weather?> FindAsync(Expression<Func<Weather, bool>> predicate)
        {
            return await _context.Weathers.FirstOrDefaultAsync(predicate);
        }

        public void Remove(Weather weather) => _context.Weathers.Remove(weather);

        public void Update(Weather weather) => _context.Weathers.Update(weather);



    }
}
