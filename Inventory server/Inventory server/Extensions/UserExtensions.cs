
using InventoryServer.Context.Requests;
using InventoryServer.Context.Respones;
using InventoryServer.Domain.Entities;

namespace InventoryServer.Context.Extensions
{
    public static class UserExtensions
    {
        public static UserResponse ToResponse(this User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                PasswordHash = user.PasswordHash,
                UserRole = user.UserRole
            };
        }

        public static User ToEntity(this UserRequest user)
        {
            return new User
            {
                Name = user.Name,
                PasswordHash = user.PasswordHash,
                UserRole = user.UserRole
            };
        }
    }
}
