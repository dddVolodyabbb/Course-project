using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Context.Providers.RawMaterialProducers;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Services.JwtToken;

namespace InventoryServer.Commands.RawMaterialProducer
{
	public class GetAllRawMaterialProducer : AuthorizationCommand
	{
		public override string Path => @"/RawMaterialProducer/GetAll";
		public override HttpMethod Method => HttpMethod.Get;
		public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin, UserRole.WarehouseUser };
		private readonly IRawMaterialProducerProvider _rawMaterialProducerProvider;
		public GetAllRawMaterialProducer(IJwtTokenService jwtTokenService, IRawMaterialProducerProvider rawMaterialProducerProvider) :
			base(jwtTokenService)
		{
			_rawMaterialProducerProvider = rawMaterialProducerProvider;
		}

		protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
		{
			var rawMaterialProducerCollection = await _rawMaterialProducerProvider.GetAllRawMaterialProducerAsync();
			var response = rawMaterialProducerCollection.Select(rawMaterialProducer => rawMaterialProducer.ToResponse()).ToList();
			await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(response)).ConfigureAwait(false);
		}
	}
}