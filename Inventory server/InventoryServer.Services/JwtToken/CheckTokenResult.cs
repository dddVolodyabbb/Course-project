using InventoryServer.Domain.Entities;

namespace InventoryServer.Services.JwtToken
{
    public class CheckTokenResult
    {
        public bool IsFaulted { get; set; }
        public string UserId { get; set; }
        public UserRole UserRole { get; set; }
    }
}
