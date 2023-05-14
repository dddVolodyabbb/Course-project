using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Commands;
using InventoryServer.Context.Providers;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Requests;
using InventoryServer.Services.JwtToken;

namespace Inventory_server.Commands.RawMaterialType;

public class UpdateRawMaterialType : AuthorizationCommand
{
    private const string RawMaterialTypeName = "RawMaterialTypeName";
    private readonly IRawMaterialTypeProvider _companyProvider;

    public UpdateRawMaterialType(IJwtTokenService jwtTokenService, IRawMaterialTypeProvider companyProvider) : base(
        jwtTokenService)
    {
        _companyProvider = companyProvider;
    }

    public override string Path => @$"/RawMaterialType/Update?Name=(?<{RawMaterialTypeName}>.+)";
    public override HttpMethod Method => HttpMethod.Put;
    public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };

    protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
    {
        var rawMaterialTypeName = path.Groups[RawMaterialTypeName].Value;
        if (rawMaterialTypeName is "" or "RawMaterialTypeName")
            await context.WriteResponseAsync(404, "Не введенно название компании").ConfigureAwait(false);

        var rawMaterialType = await _companyProvider.GetOneRawMaterialTypeAsync(rawMaterialTypeName);
        if (rawMaterialType is null)
        {
            await context.WriteResponseAsync(404, $"Компании \"{rawMaterialTypeName}\" не существует в базе данных")
                .ConfigureAwait(false);
            return;
        }

        var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
        if (!JsonSerializeHelper.TryDeserialize<RawMaterialTypeRequest>(requestBody, out var rawMaterialTypeRequest))
        {
            await context.WriteResponseAsync(400, "Недопустимое содержимое тела запроса").ConfigureAwait(false);
            return;
        }

        var newRawMaterialType = rawMaterialTypeRequest.ToEntity();

        await _companyProvider.UpdateRawMaterialTypeAsync(rawMaterialType, newRawMaterialType).ConfigureAwait(false);
        await context.WriteResponseAsync(201).ConfigureAwait(false);
    }
}