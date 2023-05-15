using InventoryServer.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryServer.Context.Providers
{
	public interface IUserProvider
	{
		Task<ICollection<User>> GetAllUserAsync();
		Task<User> GetUserByNameAsync(string name);
		Task<User> GetOneUserAsync(int userId);
		Task CreateUserAsync(User user);
		Task UpdateUserAsync(int id, User newUser);
		Task DeleteUserAsync(int id);
	}
}