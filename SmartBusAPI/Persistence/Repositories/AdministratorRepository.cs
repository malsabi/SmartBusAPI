using SmartBusAPI.Entities;
using Microsoft.EntityFrameworkCore;
using SmartBusAPI.Common.Interfaces.Repositories;

namespace SmartBusAPI.Persistence.Repositories
{
    public class AdministratorRepository : IAdministratorRepository
    {
        private readonly SmartBusContext smartBusContext;

        public AdministratorRepository(SmartBusContext smartBusContext)
        {
            this.smartBusContext = smartBusContext;
        }

        public async Task<IEnumerable<Administrator>> GetAdministrators()
        {
            return await smartBusContext.Administrators.ToListAsync();
        }

        public async Task<Administrator> GetAdministratorById(int administratorId)
        {
            return await smartBusContext.Administrators.FindAsync(administratorId);
        }

        public async Task AddAdministrator(Administrator administrator)
        {
            smartBusContext.Administrators.Add(administrator);
            await smartBusContext.SaveChangesAsync();
        }

        public async Task UpdateAdministrator(Administrator administrator)
        {
            smartBusContext.Administrators.Update(administrator);
            await smartBusContext.SaveChangesAsync();
        }

        public async Task DeleteAdministrator(Administrator administrator)
        {
            smartBusContext.Administrators.Remove(administrator);
            await smartBusContext.SaveChangesAsync();
        }
    }
}