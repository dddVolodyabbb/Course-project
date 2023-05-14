
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
        private const string WarehouseName = "WarehouseName";
        public override string Path => @$"/Warehouse/GetOne?Name=(?<{WarehouseName}>.+)";
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
            var warehouseName = path.Groups[WarehouseName].Value;
            if (warehouseName is "" or "WarehouseName")
                await context.WriteResponseAsync(404, "Не введенно название компании").ConfigureAwait(false);
            var response = await _companyProvider.GetOneWarehouseAsync(warehouseName);

            await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(response)).ConfigureAwait(false);
        }
    }
}