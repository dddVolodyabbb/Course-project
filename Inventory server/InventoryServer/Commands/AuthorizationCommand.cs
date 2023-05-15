
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Services.JwtToken;

namespace InventoryServer.Commands
{
    public abstract class AuthorizationCommand : ICommand
    {
        private const string AuthorizationHeaderKey = @"/Authorization";
        public abstract string Path { get; }
        public abstract HttpMethod Method { get; }
        public abstract UserRole[] AllowedUserRoles { get; }

        private readonly IJwtTokenService _jwtTokenService;

        protected AuthorizationCommand(IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        public async Task HandleRequestAsync(HttpListenerContext context, Match path)
        {
            var checkTokenResult = _jwtTokenService.CheckToken(context.Request.Headers[AuthorizationHeaderKey]);
            if (checkTokenResult.IsFaulted)
            {
                await context.WriteResponseAsync(401, "Не авторизован").ConfigureAwait(false);
                return;
            }

            if (!AllowedUserRoles.Contains(checkTokenResult.UserRole))
            {
                await context.WriteResponseAsync(403, "Отказано в доступе").ConfigureAwait(false);
                return;
            }

            await HandleRequestInternalAsync(context, path).ConfigureAwait(false);
        }

        protected abstract Task HandleRequestInternalAsync(HttpListenerContext context, Match path);
    }
}
