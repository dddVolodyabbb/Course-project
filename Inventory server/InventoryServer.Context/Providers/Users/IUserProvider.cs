using InventoryServer.Context.Contexts;
using InventoryServer.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryServer.Context.Providers
{
    public interface IUserProvider
    {
        public ContextInventoryControl DbContextInventoryControl { get; }
        Task<ICollection<User>> GetAllUserAsync();
        Task<User> GetOneUserAsync(string userName);
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user, User newUser);
        Task DeleteUserAsync(User user);
    }
}
