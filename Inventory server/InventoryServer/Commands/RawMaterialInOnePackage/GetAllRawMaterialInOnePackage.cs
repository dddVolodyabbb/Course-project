using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Context.Providers.RawMaterialInOnePackages;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Services.JwtToken;

namespace InventoryServer.Commands.RawMaterialInOnePackage
{
	public class GetAllRawMaterialInOnePackage : AuthorizationCommand
	{
		public override string Path => @"/RawMaterialInOnePackage/GetAll";
		public override HttpMethod Method => HttpMethod.Get;
		public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin, UserRole.WarehouseUser };
		private readonly IRawMaterialInOnePackageProvider _rawMaterialInOnePackageProvider;

		public GetAllRawMaterialInOnePackage(IJwtTokenService jwtTokenService, IRawMaterialInOnePackageProvider rawMaterialInOnePackageProvider) :
            base(jwtTokenService)
		{
			_rawMaterialInOnePackageProvider = rawMaterialInOnePackageProvider;
		}

		protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
		{
			var rawMaterialInOnePackageCollection = await _rawMaterialInOnePackageProvider.GetAllRawMaterialInOnePackageAsync();
			var response = rawMaterialInOnePackageCollection
				.Select(rawMaterialInOnePackage => rawMaterialInOnePackage.ToResponse()).ToList();
			await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(response)).ConfigureAwait(false);
		}
	}
}