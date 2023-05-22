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
    public class GetOnePercentageOfRawMaterial : AuthorizationCommand
    {
        private const string PercentageOfRawMaterialId = "PercentageOfRawMaterialId";

        public override string Path => @$"/PercentageOfRawMaterial/GetOne?Id=(?<{PercentageOfRawMaterialId}>.+)";

        public override HttpMethod Method => HttpMethod.Get;
        public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin, UserRole.WarehouseUser };
        private readonly IPercentageOfRawMaterialProvider _percentageOfRawMaterialProvider;

        public GetOnePercentageOfRawMaterial(IJwtTokenService jwtTokenService, IPercentageOfRawMaterialProvider percentageOfRawMaterialProvider) :
            base(jwtTokenService)
        {
            _percentageOfRawMaterialProvider = percentageOfRawMaterialProvider;
        }

        protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
        {
            var id = int.Parse(path.Groups[PercentageOfRawMaterialId].Value);
            var response = await _percentageOfRawMaterialProvider.GetOnePercentageOfRawMaterialAsync(id);
            if (response is null)
            {
                await context.WriteResponseAsync(404, $"Записи под id: \"{id}\" не существует в базе данных")
                    .ConfigureAwait(false);
                return;
            }

            await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(response)).ConfigureAwait(false);
        }
    }
}