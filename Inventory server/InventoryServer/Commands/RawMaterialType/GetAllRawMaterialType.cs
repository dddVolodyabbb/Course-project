using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Context.Providers;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Services.JwtToken;

namespace InventoryServer.Commands.RawMaterialType;

public class GetAllRawMaterialType : AuthorizationCommand
{
    private readonly IRawMaterialTypeProvider _rawMaterialTypeProvider;

    public GetAllRawMaterialType(IJwtTokenService jwtTokenService, IRawMaterialTypeProvider rawMaterialTypeProvider) :
		base(jwtTokenService)
    {
        _rawMaterialTypeProvider = rawMaterialTypeProvider;
    }

    public override string Path => @"/RawMaterialType/GetAll";
    public override HttpMethod Method => HttpMethod.Get;
    public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin, UserRole.WarehouseUser };

    protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
    {
        var rawMaterialTypeCollection = await _rawMaterialTypeProvider.GetAllRawMaterialTypeAsync();
        var response = rawMaterialTypeCollection.Select(rawMaterialType => rawMaterialType.ToResponse());
		await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(response)).ConfigureAwait(false);
    }
}