using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Context.Providers.ProductInOnePackages;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Services.JwtToken;

namespace InventoryServer.Commands.ProductInOnePackage
{
	public class GetAllProductInOnePackage : AuthorizationCommand
	{
		public override string Path => @"/ProductInOnePackage/GetAll";
		public override HttpMethod Method => HttpMethod.Get;
		public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin, UserRole.WarehouseUser };
		private readonly IProductInOnePackageProvider _productInOnePackageProvider;

		public GetAllProductInOnePackage(IJwtTokenService jwtTokenService, IProductInOnePackageProvider productInOnePackageProvider) :
            base(jwtTokenService)
		{
			_productInOnePackageProvider = productInOnePackageProvider;
		}

		protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
		{
			var productInOnePackageCollection = await _productInOnePackageProvider.GetAllProductInOnePackageAsync();
			var response = productInOnePackageCollection
				.Select(productInOnePackage => productInOnePackage.ToResponse()).ToList();
			await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(response)).ConfigureAwait(false);
		}
	}
}