using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Commands;
using InventoryServer.Context.Providers.ProductTypes;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Services.JwtToken;

namespace Inventory_server.Commands.ProductType
{
	public class GetOneProductType : AuthorizationCommand
	{
		private const string ProductTypeId = "ProductTypeId";
		public override string Path => @$"/ProductType/GetOne?Id=(?<{ProductTypeId}>.+)";
		public override HttpMethod Method => HttpMethod.Get;
		public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin, UserRole.WarehouseUser };
		private readonly IProductTypeProvider _companyProvider;

		public GetOneProductType(IJwtTokenService jwtTokenService, IProductTypeProvider companyProvider) :
			base(jwtTokenService)
		{
			_companyProvider = companyProvider;
		}

		protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
		{
			var productTypeId = int.Parse(path.Groups[ProductTypeId].Value);
			var response = await _companyProvider.GetOneProductTypeAsync(productTypeId);

			await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(response)).ConfigureAwait(false);
		}
	}
}