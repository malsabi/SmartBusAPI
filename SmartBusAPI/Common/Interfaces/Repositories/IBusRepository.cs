using SmartBusAPI.Entities;

namespace SmartBusAPI.Common.Interfaces.Repositories
{
    public interface IBusRepository
    {
        public Task<IEnumerable<Bus>> GetAllBuses();
        public Task<Bus> GetBusById(int id);
        public Task AddBus(Bus bus);
        public Task UpdateBus(Bus bus);
        public Task DeleteBus(Bus bus);
    }
}