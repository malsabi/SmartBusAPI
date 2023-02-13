using SmartBusAPI.Entities;
using Microsoft.EntityFrameworkCore;
using SmartBusAPI.Common.Interfaces.Repositories;

namespace SmartBusAPI.Persistence.Repositories
{
    public class BusRepository : IBusRepository
    {
        private readonly SmartBusContext smartBusContext;

        public BusRepository(SmartBusContext smartBusContext)
        {
            this.smartBusContext = smartBusContext;
        }

        public async Task<IEnumerable<Bus>> GetAllBuses()
        {
            return await smartBusContext.Buses.ToListAsync();
        }

        public async Task<Bus> GetBusById(int id)
        {
            return await smartBusContext.Buses.FindAsync(id);
        }

        public async Task AddBus(Bus bus)
        {
            smartBusContext.Buses.Add(bus);
            await smartBusContext.SaveChangesAsync();
        }

        public async Task UpdateBus(Bus bus)
        {
            smartBusContext.Buses.Update(bus);
            await smartBusContext.SaveChangesAsync();
        }

        public async Task DeleteBus(Bus bus)
        {
            smartBusContext.Buses.Remove(bus);
            await smartBusContext.SaveChangesAsync();
        }
    }
}