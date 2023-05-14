using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Commands;
using InventoryServer.Context.Providers.Warehouses;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Requests;
using InventoryServer.Services.JwtToken;

namespace Inventory_server.Commands.Warehouse;

public class UpdateWarehouse : AuthorizationCommand
{
    private const string WarehouseName = "WarehouseName";
    private readonly IWarehouseProvider _companyProvider;

    public UpdateWarehouse(IJwtTokenService jwtTokenService, IWarehouseProvider companyProvider) : base(
        jwtTokenService)
    {
        _companyProvider = companyProvider;
    }

    public override string Path => @$"/Warehouse/Update?Name=(?<{WarehouseName}>.+)";
    public override HttpMethod Method => HttpMethod.Put;
    public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };

    protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
    {
        var warehouseName = path.Groups[WarehouseName].Value;
        if (warehouseName is "" or "WarehouseName")
            await context.WriteResponseAsync(404, "Не введенно название компании").ConfigureAwait(false);

        var warehouse = await _companyProvider.GetOneWarehouseAsync(warehouseName);
        if (warehouse is null)
        {
            await context.WriteResponseAsync(404, $"Компании \"{warehouseName}\" не существует в базе данных")
                .ConfigureAwait(false);
            return;
        }

        var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
        if (!JsonSerializeHelper.TryDeserialize<WarehouseRequest>(requestBody, out var WarehouseRequest))
        {
            await context.WriteResponseAsync(400, "Недопустимое содержимое тела запроса").ConfigureAwait(false);
            return;
        }

        var newWarehouse = WarehouseRequest.ToEntity();

        await _companyProvider.UpdateWarehouseAsync(warehouse, newWarehouse).ConfigureAwait(false);
        await context.WriteResponseAsync(201).ConfigureAwait(false);
    }
}