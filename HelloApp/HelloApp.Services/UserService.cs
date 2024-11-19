namespace HelloApp.DataAccess
{
    public class UserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IRepository<User> userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<User>> GetAllUsersAsync() => await _userRepository.GetAllAsync();

        public async Task<User?> GetUserByIdAsync(int id) => await _userRepository.GetByIdAsync(id);

        public async Task AddUserAsync(User user)
        {
            // Проверка, существует ли пользователь с таким email
            var existingUser = await _userRepository.FindAsync(u => u.Email == user.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException($"Пользователь с email {user.Email} уже существует.");
            }

            // Если пользователь с таким email не найден, добавляем нового
            await _userRepository.AddAsync(user);
        }

        public void RemoveUser(User user) => _userRepository.Remove(user);

        public Task UpdateUserAsync(User user)
        {
            _userRepository.Update(user);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync() => await _unitOfWork.SaveChangesAsync();
    }
}
