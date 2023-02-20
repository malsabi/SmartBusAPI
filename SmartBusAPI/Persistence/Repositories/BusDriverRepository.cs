namespace SmartBusAPI.Persistence.Repositories
{
    public class BusDriverRepository : IBusDriverRepository
    {
        private readonly SmartBusContext smartBusContext;

        public BusDriverRepository(SmartBusContext smartBusContext)
        {
            this.smartBusContext = smartBusContext;
        }

        public async Task<IEnumerable<BusDriver>> GetAllBusDrivers()
        {
            return await smartBusContext.BusDrivers.ToListAsync();
        }

        public async Task<BusDriver> GetBusDriverById(int id)
        {
            return await smartBusContext.BusDrivers.FindAsync(id);
        }

        public async Task<BusDriver> GetBusDriverByDriverID(string driverID)
        {
            return await smartBusContext.BusDrivers.FirstOrDefaultAsync(b => b.DriverID == driverID);
        }

        public async Task AddBusDriver(BusDriver busDriver)
        {
            smartBusContext.BusDrivers.Add(busDriver);
            await smartBusContext.SaveChangesAsync();
        }

        public async Task UpdateBusDriver(BusDriver busDriver)
        {
            smartBusContext.BusDrivers.Update(busDriver);
            await smartBusContext.SaveChangesAsync();
        }

        public async Task DeleteBusDriver(BusDriver busDriver)
        {
            smartBusContext.BusDrivers.Update(busDriver);
            await smartBusContext.SaveChangesAsync();
        }
    }
}