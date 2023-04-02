using SmartBusAPI.Common.Extensions;

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

        public async Task<Administrator> GetAdministratorByEmail(string email)
        {
            return await smartBusContext.Administrators.FirstOrDefaultAsync(admin => admin.Email == email);
        }

        public async Task AddAdministrator(Administrator administrator)
        {
            smartBusContext.Administrators.Add(administrator);
            await smartBusContext.SaveChangesAsync();
        }

        public async Task UpdateAdministrator(Administrator administrator)
        {
            if (await smartBusContext.Administrators.FirstOrDefaultAsync(p => p.ID == administrator.ID) is Administrator found)
            {
                found.FirstName = administrator.FirstName ?? found.FirstName;
                found.LastName = administrator.LastName ?? found.LastName;
                found.Email = administrator.Email ?? found.Email;
                found.Password = administrator.Password ?? found.Password;
            }
            await smartBusContext.SaveChangesAsync();
        }

        public async Task DeleteAdministrator(Administrator administrator)
        {
            smartBusContext.Administrators.Remove(administrator);
            await smartBusContext.SaveChangesAsync();
            smartBusContext.ResetIdentityValue<Administrator>("Administrators");
        }
    }
}