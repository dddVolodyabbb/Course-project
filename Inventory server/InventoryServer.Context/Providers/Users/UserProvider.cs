using System;
using InventoryServer.Context.Contexts;
using InventoryServer.Domain.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace InventoryServer.Context.Providers.Users
{
    public class UserProvider : IUserProvider
    {
        public ContextInventoryControl DbContextInventoryControl => new();
        public async Task<ICollection<User>> GetAllUserAsync()
        {
            return await DbContextInventoryControl.Users.ToListAsync().ConfigureAwait(false);
        }

        public async Task<User> GetOneUserAsync(string userName)
        {
            try
            {
                return await DbContextInventoryControl.Users
                    .FirstAsync(d => d.Name == userName)
                    .ConfigureAwait(false);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task CreateUserAsync(User user)
        {
            DbContextInventoryControl.Users.Add(user);
            await DbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateUserAsync(User user, User newUser)
        {
            user.Name = newUser.Name;
            user.PasswordHash = newUser.PasswordHash;
            user.UserRole = newUser.UserRole;
            await DbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteUserAsync(User user)
        {
            DbContextInventoryControl.Users.Remove(user);
            await DbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
