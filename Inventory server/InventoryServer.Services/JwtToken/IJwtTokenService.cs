using InventoryServer.Domain.Entities;

namespace InventoryServer.Services.JwtToken
{
    public interface IJwtTokenService
    {
        string CreateToken(User user);
        CheckTokenResult CheckToken(string token);
    }
}
