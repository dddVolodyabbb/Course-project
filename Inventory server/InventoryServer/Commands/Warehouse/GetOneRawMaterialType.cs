
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Commands;
using InventoryServer.Context.Providers.Warehouses;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Services.JwtToken;

namespace Inventory_server.Commands.Warehouse
{
    public class GetOneWarehouse : AuthorizationCommand
    {
        private const string WarehouseId = "WarehouseId";
        public override string Path => @$"/Warehouse/GetOne?Id=(?<{WarehouseId}>.+)";
        public override HttpMethod Method => HttpMethod.Get;
        public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin, UserRole.WarehouseUser };
        private readonly IWarehouseProvider _companyProvider;

        public GetOneWarehouse(IJwtTokenService jwtTokenService, IWarehouseProvider companyProvider) : base(
            jwtTokenService)
        {
            _companyProvider = companyProvider;
        }

        protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
        {
            var warehouseId = int.Parse(path.Groups[WarehouseId].Value);
			var response = await _companyProvider.GetOneWarehouseAsync(warehouseId);
			await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(response)).ConfigureAwait(false);
        }
    }
}