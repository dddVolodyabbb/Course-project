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
        
        public async Task<ICollection<User>> GetAllUserAsync()
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            return await dbContextInventoryControl.Users.ToListAsync().ConfigureAwait(false);
        }

        public async Task<User> GetOneUserAsync(string userName)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            return await dbContextInventoryControl.Users
                .FirstAsync(d => d.Name == userName)
                .ConfigureAwait(false);
        }

        public async Task CreateUserAsync(User user)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            dbContextInventoryControl.Users.Add(user);
            await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateUserAsync(User user, User newUser)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            user.Name = newUser.Name;
            user.PasswordHash = newUser.PasswordHash;
            user.UserRole = newUser.UserRole;
            await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteUserAsync(User user)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            dbContextInventoryControl.Users.Remove(user);
            await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
