using SmartBusAPI.Common.Extensions;

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
            if (await smartBusContext.BusDrivers.FirstOrDefaultAsync(p => p.ID == busDriver.ID) is BusDriver found)
            {
                found.FirstName = busDriver.FirstName;
                found.LastName = busDriver.LastName;
                found.Email = busDriver.Email;
                found.DriverID = busDriver.DriverID;
                found.PhoneNumber = busDriver.PhoneNumber;
                found.Country = busDriver.Country;
                found.Password = busDriver.Password;
                found.BusID = busDriver.BusID;
            }
            await smartBusContext.SaveChangesAsync();
        }

        public async Task DeleteBusDriver(BusDriver busDriver)
        {
            smartBusContext.BusDrivers.Remove(busDriver);
            await smartBusContext.SaveChangesAsync();
            smartBusContext.ResetIdentityValue<BusDriver>("BusDrivers");
        }
    }
}