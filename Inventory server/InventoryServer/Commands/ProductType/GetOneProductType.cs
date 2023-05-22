using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Context.Providers.ProductTypes;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Services.JwtToken;

namespace InventoryServer.Commands.ProductType
{
	public class GetOneProductType : AuthorizationCommand
	{
		private const string ProductTypeId = "ProductTypeId";
		public override string Path => @$"/ProductType/GetOne?Id=(?<{ProductTypeId}>.+)";
		public override HttpMethod Method => HttpMethod.Get;
		public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin, UserRole.WarehouseUser };
		private readonly IProductTypeProvider _productTypeProvider;

		public GetOneProductType(IJwtTokenService jwtTokenService, IProductTypeProvider productTypeProvider) :
			base(jwtTokenService)
		{
			_productTypeProvider = productTypeProvider;
		}

		protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
		{
			var productTypeId = int.Parse(path.Groups[ProductTypeId].Value);
			var response = await _productTypeProvider.GetOneProductTypeAsync(productTypeId);
			await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(response.ToResponse())).ConfigureAwait(false);
		}
	}
}