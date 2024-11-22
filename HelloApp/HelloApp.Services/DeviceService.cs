using AutoMapper;

namespace HelloApp.Services
{
    public class DeviceService
    {
        private readonly IRepository<DataAccess.Device> _deviceRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeviceService(IRepository<DataAccess.Device> deviceRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _deviceRepository = deviceRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<Device>> GetAllDevicesAsync()
        {
            var dataDevices = await _deviceRepository.GetAllAsync();
            return _mapper.Map<List<Device>>(dataDevices);
        }

        public async Task<Device?> GetDeviceByIdAsync(int id)
        {
            var dataDevice = await _deviceRepository.GetByIdAsync(id);
            return dataDevice == null ? null : _mapper.Map<Device>(dataDevice);
        }

        public async Task AddDeviceAsync(Device serviceDevice)
        {
            var dataDevice = _mapper.Map<DataAccess.Device>(serviceDevice);
            await _deviceRepository.AddAsync(dataDevice);
        }

        public void RemoveDevice(Device serviceDevice)
        {
            var dataDevice = _mapper.Map<DataAccess.Device>(serviceDevice);
            _deviceRepository.Remove(dataDevice);
        }

        public async Task UpdateDeviceAsync(Device serviceDevice)
        {
            var dataDevice = _mapper.Map<DataAccess.Device>(serviceDevice);
            _deviceRepository.Update(dataDevice);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task SaveChangesAsync() => await _unitOfWork.SaveChangesAsync();
    }
}
