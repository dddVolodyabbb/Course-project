using System.Linq;
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
	public class GetAllProductType : AuthorizationCommand
	{
		public override string Path => @"/ProductType/GetAll";
		public override HttpMethod Method => HttpMethod.Get;
		public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin, UserRole.WarehouseUser };
		private readonly IProductTypeProvider _productTypeProvider;
		public GetAllProductType(IJwtTokenService jwtTokenService, IProductTypeProvider productTypeProvider) : base(jwtTokenService)
		{
			_productTypeProvider = productTypeProvider;
		}

		protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
		{
			var productTypeCollection = await _productTypeProvider.GetAllProductTypeAsync();
			var response = productTypeCollection.Select(productType => productType.ToResponse()).ToList();
			await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(response)).ConfigureAwait(false);
		}
	}
}