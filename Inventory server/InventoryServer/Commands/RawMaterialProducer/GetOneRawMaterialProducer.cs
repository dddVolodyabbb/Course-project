using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Commands;
using InventoryServer.Context.Providers.RawMaterialProducers;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Services.JwtToken;

namespace Inventory_server.Commands.RawMaterialProducer
{
	public class GetOneRawMaterialProducer : AuthorizationCommand
	{
		private const string RawMaterialProducerId = "RawMaterialProducerId";
		public override string Path => @$"/RawMaterialProducer/GetOne?Id=(?<{RawMaterialProducerId}>.+)";
		public override HttpMethod Method => HttpMethod.Get;
		public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin, UserRole.WarehouseUser };
		private readonly IRawMaterialProducerProvider _companyProvider;

		public GetOneRawMaterialProducer(IJwtTokenService jwtTokenService, IRawMaterialProducerProvider companyProvider) : 
			base(jwtTokenService)
		{
			_companyProvider = companyProvider;
		}

		protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
		{
			var rawMaterialProducerId = int.Parse(path.Groups[RawMaterialProducerId].Value);
			var response = await _companyProvider.GetOneRawMaterialProducerAsync(rawMaterialProducerId);
			await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(response)).ConfigureAwait(false);
		}
	}
}