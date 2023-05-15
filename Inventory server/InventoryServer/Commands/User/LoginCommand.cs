using InventoryServer.Helpers;
using InventoryServer.Services.Crypt;
using InventoryServer.Services.JwtToken;
using System.Net.Http;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Commands;
using InventoryServer.Context.Providers;
using InventoryServer.Extensions;
using InventoryServer.Requests;

namespace Inventory_server.Commands.User
{
    public class LoginCommand : ICommand
    {
        public string Path => @"/Login";
        public HttpMethod Method => HttpMethod.Post;

        private readonly ICryptService _cryptService;
        private readonly IUserProvider _usersProvider;
        private readonly IJwtTokenService _jwtTokenService;

        public LoginCommand(ICryptService cryptService, IUserProvider usersProvider, IJwtTokenService jwtTokenService)
        {
            _cryptService = cryptService;
            _usersProvider = usersProvider;
            _jwtTokenService = jwtTokenService;
        }

        public async Task HandleRequestAsync(HttpListenerContext context, Match path)
        {
            var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
            if (!JsonSerializeHelper.TryDeserialize<LoginRequest>(requestBody, out var loginRequest))
            {
                await context.WriteResponseAsync(400, "Недопустимое содержимое тела запроса").ConfigureAwait(false);
                return;
            }

            var user = await _usersProvider.GetUserByNameAsync(loginRequest.Name).ConfigureAwait(false);
            if (user == null)
            {
                await context.WriteResponseAsync(400, "Неправильный логин").ConfigureAwait(false);
                return;
            }

            var passwordHash = _cryptService.ComputeHash(loginRequest.Password);
            if (user.PasswordHash != passwordHash)
            {
                await context.WriteResponseAsync(400, "Неправильный пароль").ConfigureAwait(false);
                return;
            }

            var token = _jwtTokenService.CreateToken(user);
            await context.WriteResponseAsync(200, token).ConfigureAwait(false);
        }
    }
}
