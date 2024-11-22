using HelloApp.DataAccess;

public class DeviceService
{
    private readonly IRepository<Device> _deviceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeviceService(IRepository<Device> deviceRepository, IUnitOfWork unitOfWork)
    {
        _deviceRepository = deviceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Device>> GetAllDevicesAsync() => await _deviceRepository.GetAllAsync();

    public async Task<Device?> GetDeviceByIdAsync(int id) => await _deviceRepository.GetByIdAsync(id);

    public async Task AddDeviceAsync(Device device) => await _deviceRepository.AddAsync(device);

    public void RemoveDevice(Device device) => _deviceRepository.Remove(device);

    public Task UpdateDeviceAsync(Device device)
    {
        _deviceRepository.Update(device);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync() => await _unitOfWork.SaveChangesAsync();
}
