namespace SmartBusAPI.Common.Interfaces.Repositories
{
    public interface IBusDriverRepository
    {
        public Task<IEnumerable<BusDriver>> GetAllBusDrivers();
        public Task<BusDriver> GetBusDriverById(int id);
        public Task<BusDriver> GetBusDriverByDriverID(string driverId);
        public Task AddBusDriver(BusDriver busDriver);
        public Task UpdateBusDriver(BusDriver busDriver);
        public Task DeleteBusDriver(BusDriver busDriver);
    }
}