using SmartBusAPI.Common.Extensions;

namespace SmartBusAPI.Persistence.Repositories
{
    public class ParentRepository : IParentRepository
    {
        private readonly SmartBusContext smartBusContext;
        
        public ParentRepository(SmartBusContext smartBusContext)
        {
            this.smartBusContext = smartBusContext;
        }

        public async Task<IEnumerable<Parent>> GetAllParents()
        {
            return await smartBusContext.Parents.ToListAsync();
        }

        public async Task<Parent> GetParentById(int id)
        {
            return await smartBusContext.Parents.FindAsync(id);
        }

        public async Task<Parent> GetParentByEmail(string email)
        {
            return await smartBusContext.Parents.FirstOrDefaultAsync(p => p.Email == email);
        }

        public async Task AddParent(Parent parent)
        {
            smartBusContext.Parents.Add(parent);
            await smartBusContext.SaveChangesAsync();
        }

        public async Task UpdateParent(Parent parent)
        {
            if (await smartBusContext.Parents.FirstOrDefaultAsync(p => p.ID == parent.ID) is Parent found)
            {
                found.FirstName = parent.FirstName;
                found.LastName = parent.LastName;
                found.Email = parent.Email;
                found.PhoneNumber = parent.PhoneNumber;
                found.Address = parent.Address;
                found.Password = parent.Password;
            }
            await smartBusContext.SaveChangesAsync();
        }

        public async Task DeleteParent(Parent parent)
        {
            smartBusContext.Parents.Remove(parent);
            await smartBusContext.SaveChangesAsync();
            smartBusContext.ResetIdentityValue<Parent>("parents");
        }
    }
}