using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Context.Providers.Warehouses;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Requests;
using InventoryServer.Services.JwtToken;

namespace InventoryServer.Commands.Warehouse;

public class CreateWarehouse : AuthorizationCommand
{
	public override string Path => @"/Warehouse/Create";
	public override HttpMethod Method => HttpMethod.Post;
	public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };
    private readonly IWarehouseProvider _warehouseProvider;

    public CreateWarehouse(IJwtTokenService jwtTokenService, IWarehouseProvider warehouseProvider) :
        base(jwtTokenService)
    {
        _warehouseProvider = warehouseProvider;
    }

    protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
    {
        var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
        if (!JsonSerializeHelper.TryDeserialize<WarehouseRequest>(requestBody, out var warehouseRequest))
        {
            await context.WriteResponseAsync(400, "Недопустимое содержимое тела запроса").ConfigureAwait(false);
            return;
        }

        var warehouse = warehouseRequest.ToEntity();
        await _warehouseProvider.CreateWarehouseAsync(warehouse).ConfigureAwait(false);
        await context.WriteResponseAsync(201).ConfigureAwait(false);
    }
}