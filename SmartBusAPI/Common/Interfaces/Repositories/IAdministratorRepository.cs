namespace SmartBusAPI.Common.Interfaces.Repositories
{
    public interface IAdministratorRepository
    {
        public Task<IEnumerable<Administrator>> GetAdministrators();
        public Task<Administrator> GetAdministratorById(int administratorId);
        public Task AddAdministrator(Administrator administrator);
        public Task UpdateAdministrator(Administrator administrator);
        public Task DeleteAdministrator(Administrator administrator);
    }
}