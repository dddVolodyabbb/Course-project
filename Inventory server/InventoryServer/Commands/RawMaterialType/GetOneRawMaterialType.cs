using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Commands;
using InventoryServer.Context.Providers;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Services.JwtToken;

namespace Inventory_server.Commands.RawMaterialType
{
	public class GetOneRawMaterialType : AuthorizationCommand
	{
		private const string RawMaterialTypeId = "RawMaterialTypeId";
		public override string Path => @$"/RawMaterialType/GetOne?Id=(?<{RawMaterialTypeId}>.+)";
		public override HttpMethod Method => HttpMethod.Get;
		public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin, UserRole.WarehouseUser };
		private readonly IRawMaterialTypeProvider _companyProvider;

		public GetOneRawMaterialType(IJwtTokenService jwtTokenService, IRawMaterialTypeProvider companyProvider) : base(
			jwtTokenService)
		{
			_companyProvider = companyProvider;
		}

		protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
		{
			var rawMaterialTypeId = int.Parse(path.Groups[RawMaterialTypeId].Value);
			var response = await _companyProvider.GetOneRawMaterialTypeAsync(rawMaterialTypeId);
			await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(response)).ConfigureAwait(false);
		}
	}
}