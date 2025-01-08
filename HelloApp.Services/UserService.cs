using AutoMapper;
using HelloApp.Services.Models;

namespace HelloApp.Services
{
    public class UserService
    {
        private readonly IRepository<DataAccess.DbUser> _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IRepository<DataAccess.DbUser> userRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<List<User>>(users);
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user == null ? null : _mapper.Map<User>(user);
        }

        public async Task AddUserAsync(User user)
        {
            var existingUser = await _userRepository.FindAsync(u => u.Email == user.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException($"Пользователь с email {user.Email} уже существует.");
            }

            var userEntity = _mapper.Map<DataAccess.DbUser>(user);
            await _userRepository.AddAsync(userEntity);
        }

        public async Task RemoveUserAsync(User user)
        {
            var userEntity = await _userRepository.GetByIdAsync(user.Id);
            if (userEntity == null)
            {
                throw new InvalidOperationException("Пользователь не найден.");
            }

            _userRepository.Remove(userEntity);
        }

        public async Task UpdateUserAsync(User user)
        {
            var existingUser = await _userRepository.GetByIdAsync(user.Id);
            if (existingUser == null)
            {
                throw new InvalidOperationException("Пользователь не найден.");
            }

            _mapper.Map(user, existingUser); // Обновляем данные из модели в сущности
            _userRepository.Update(existingUser);
        }

        public async Task SaveChangesAsync() => await _unitOfWork.SaveChangesAsync();
    }
}
