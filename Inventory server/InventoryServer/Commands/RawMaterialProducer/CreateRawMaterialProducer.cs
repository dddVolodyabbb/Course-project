using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Context.Providers.RawMaterialProducers;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Requests;
using InventoryServer.Services.JwtToken;

namespace InventoryServer.Commands.RawMaterialProducer
{
	public class CreateRawMaterialProducer : AuthorizationCommand
	{
		public override string Path => @"/RawMaterialProducer/Create";
		public override HttpMethod Method => HttpMethod.Post;
		public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };
		private readonly IRawMaterialProducerProvider _rawMaterialProducerProvider;
		public CreateRawMaterialProducer(IJwtTokenService jwtTokenService, IRawMaterialProducerProvider rawMaterialProducerProvider) :
			base(jwtTokenService)
		{
			_rawMaterialProducerProvider = rawMaterialProducerProvider;
		}

		protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
		{
			var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
			if (!JsonSerializeHelper.TryDeserialize<RawMaterialProducerRequest>(requestBody, out var rawMaterialProducerRequest))
			{
				await context.WriteResponseAsync(400, "Недопустимое содержимое тела запроса").ConfigureAwait(false);
				return;
			}

			var rawMaterialProducer = rawMaterialProducerRequest.ToEntity();
			await _rawMaterialProducerProvider.CreateRawMaterialProducerAsync(rawMaterialProducer).ConfigureAwait(false);
			await context.WriteResponseAsync(201, null).ConfigureAwait(false);
		}
	}
}