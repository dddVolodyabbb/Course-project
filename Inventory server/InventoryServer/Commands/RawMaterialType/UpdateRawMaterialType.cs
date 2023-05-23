using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Context.Providers;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Requests;
using InventoryServer.Services.JwtToken;

namespace InventoryServer.Commands.RawMaterialType;

public class UpdateRawMaterialType : AuthorizationCommand
{
    private const string RawMaterialTypeId = "RawMaterialTypeId";
    private readonly IRawMaterialTypeProvider _rawMaterialTypeProvider;

    public UpdateRawMaterialType(IJwtTokenService jwtTokenService, IRawMaterialTypeProvider rawMaterialTypeProvider) :
		base(jwtTokenService)
    {
        _rawMaterialTypeProvider = rawMaterialTypeProvider;
    }

    public override string Path => @$"/RawMaterialType/Update?Id=(?<{RawMaterialTypeId}>.+)";
    public override HttpMethod Method => HttpMethod.Put;
    public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };

    protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
    {
        var rawMaterialTypeId = int.Parse(path.Groups[RawMaterialTypeId].Value);
		var rawMaterialType = await _rawMaterialTypeProvider.GetOneRawMaterialTypeAsync(rawMaterialTypeId);
        if (rawMaterialType is null)
        {
            await context.WriteResponseAsync(404, $"Компании \"{rawMaterialTypeId}\" не существует в базе данных")
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
		await _rawMaterialTypeProvider.UpdateRawMaterialTypeAsync(rawMaterialTypeId, newRawMaterialType).ConfigureAwait(false);
        await context.WriteResponseAsync(201).ConfigureAwait(false);
    }
}