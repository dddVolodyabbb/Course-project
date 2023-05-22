using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Context.Providers.PercentageOfRawMaterials;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Services.JwtToken;

namespace InventoryServer.Commands.PercentageOfRawMaterial
{
	public class GetAllPercentageOfRawMaterial : AuthorizationCommand
	{
		public override string Path => @"/PercentageOfRawMaterial/GetAll";
		public override HttpMethod Method => HttpMethod.Get;
		public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin, UserRole.WarehouseUser };
		private readonly IPercentageOfRawMaterialProvider _percentageOfRawMaterialProvider;

		public GetAllPercentageOfRawMaterial(IJwtTokenService jwtTokenService, IPercentageOfRawMaterialProvider percentageOfRawMaterialProvider) :
            base(jwtTokenService)
		{
			_percentageOfRawMaterialProvider = percentageOfRawMaterialProvider;
		}

		protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
		{
			var percentageOfRawMaterialCollection = await _percentageOfRawMaterialProvider.GetAllPercentageOfRawMaterialAsync();
			var response = percentageOfRawMaterialCollection
				.Select(percentageOfRawMaterial => percentageOfRawMaterial.ToResponse()).ToList();
			await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(response)).ConfigureAwait(false);
		}
	}
}