
using InventoryServer.Domain.Entities;

namespace InventoryServer.Context.Respones
{
    public class UserResponse
    {
        public int Id { get; set; }
      
        public string Name { get; set; }
        
        public string PasswordHash { get; set; }
        
        public UserRole UserRole { get; set; }
    }
}
