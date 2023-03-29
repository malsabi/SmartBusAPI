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
            if (await smartBusContext.Buses.FirstOrDefaultAsync(p => p.ID == bus.ID) is Bus found)
            {
                found.LicenseNumber = bus.LicenseNumber;
                found.Capacity = bus.Capacity;
                found.CurrentLocation = bus.CurrentLocation ?? found.CurrentLocation;
                found.DestinationType = bus.DestinationType;
                found.IsInService = bus.IsInService;
            }
            await smartBusContext.SaveChangesAsync();
        }

        public async Task DeleteBus(Bus bus)
        {
            smartBusContext.Buses.Remove(bus);
            await smartBusContext.SaveChangesAsync();
        }
    }
}