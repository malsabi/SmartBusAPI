namespace SmartBusAPI.Common.Interfaces.Repositories
{
    public interface IParentRepository
    {
        public Task<IEnumerable<Parent>> GetAllParents();
        public Task<Parent> GetParentById(int id);
        public Task<Parent> GetParentByEmail(string email);
        public Task AddParent(Parent parent);
        public Task UpdateParent(Parent parent);
        public Task DeleteParent(Parent parent);
    }
}