using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Context.Providers.Warehouses;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Services.JwtToken;

namespace InventoryServer.Commands.Warehouse
{
	public class GetAllWarehouse : AuthorizationCommand
	{
		public override string Path => @"/Warehouse/GetAll";
		public override HttpMethod Method => HttpMethod.Get;
		public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin, UserRole.WarehouseUser };
		private readonly IWarehouseProvider _warehouseProvider;
		public GetAllWarehouse(IJwtTokenService jwtTokenService, IWarehouseProvider warehouseProvider) :
			base(jwtTokenService)
		{
			_warehouseProvider = warehouseProvider;
		}

		protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
		{
			var warehouseCollection = await _warehouseProvider.GetAllWarehouseAsync();
			var response = warehouseCollection.Select(warehouse => warehouse.ToResponse()).ToList();
			await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(response)).ConfigureAwait(false);
		}
	}
}