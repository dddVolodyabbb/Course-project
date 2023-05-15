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

public class CreateRawMaterialType : AuthorizationCommand
{
    private readonly IRawMaterialTypeProvider _companyProvider;

    public CreateRawMaterialType(IJwtTokenService jwtTokenService, IRawMaterialTypeProvider companyProvider) : 
        base(jwtTokenService)
    {
        _companyProvider = companyProvider;
    }

    public override string Path => @"/RawMaterialType/Create";
    public override HttpMethod Method => HttpMethod.Post;
    public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };

    protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
    {
        var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
        if (!JsonSerializeHelper.TryDeserialize<RawMaterialTypeRequest>(requestBody, out var rawMaterialTypeRequest))
        {
            await context.WriteResponseAsync(400, "Недопустимое содержимое тела запроса").ConfigureAwait(false);
            return;
        }

        var rawMaterialType = rawMaterialTypeRequest.ToEntity();
        await _companyProvider.CreateRawMaterialTypeAsync(rawMaterialType).ConfigureAwait(false);
        await context.WriteResponseAsync(201).ConfigureAwait(false);
    }
}