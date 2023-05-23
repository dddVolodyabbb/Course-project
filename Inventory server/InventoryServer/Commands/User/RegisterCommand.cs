using InventoryServer.Domain.Entities;
using InventoryServer.Helpers;
using InventoryServer.Services.Crypt;
using System.Net.Http;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Context.Providers;
using InventoryServer.Extensions;
using InventoryServer.Requests;

namespace InventoryServer.Commands.User
{
	public class RegisterCommand : ICommand
	{
		public string Path => @"/Register";
		public HttpMethod Method => HttpMethod.Post;
		private readonly ICryptService _cryptService;
		private readonly IUserProvider _usersProvider;

		public RegisterCommand(ICryptService cryptService, IUserProvider usersProvider)
		{
			_cryptService = cryptService;
			_usersProvider = usersProvider;
		}

		public async Task HandleRequestAsync(HttpListenerContext context, Match path)
		{
			var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
			if (!JsonSerializeHelper.TryDeserialize<RegisterRequest>(requestBody, out var registerRequest))
			{
				await context.WriteResponseAsync(400, "Недопустимое содержимое тела запроса").ConfigureAwait(false);
				return;
			}

			var passwordHash = _cryptService.ComputeHash(registerRequest.Password);
			var newUser = new InventoryServer.Domain.Entities.User
			{
				Name = registerRequest.Name,
				PasswordHash = passwordHash,
				UserRole = UserRole.DefaultUser
			};

			await _usersProvider.CreateUserAsync(newUser).ConfigureAwait(false);
			await context.WriteResponseAsync(200).ConfigureAwait(false);
		}
	}
}