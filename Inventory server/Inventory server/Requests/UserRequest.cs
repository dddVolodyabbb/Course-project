using InventoryServer.Domain.Entities;

namespace InventoryServer.Context.Requests
{
    public class UserRequest
    {
        public string Name { get; set; }
        
        public string PasswordHash { get; set; }
        
        public UserRole UserRole { get; set; }
    }
}
