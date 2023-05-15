using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Commands;
using InventoryServer.Context.Providers.HistoryDefectiveRavMaterials;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Services.JwtToken;

namespace Inventory_server.Commands.HistoryDefectiveRavMaterial
{
    public class GetOneHistoryDefectiveRavMaterial : AuthorizationCommand
    {
        private const string HistoryDefectiveRavMaterialId = "HistoryDefectiveRavMaterialId";

        public override string Path =>
            @$"/HistoryDefectiveRavMaterial/GetOne?Id=(?<{HistoryDefectiveRavMaterialId}>.+)";

        public override HttpMethod Method => HttpMethod.Get;
        public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin, UserRole.WarehouseUser };
        private readonly IHistoryDefectiveRavMaterialProvider _companyProvider;

        public GetOneHistoryDefectiveRavMaterial(IJwtTokenService jwtTokenService,
            IHistoryDefectiveRavMaterialProvider companyProvider) :
            base(jwtTokenService)
        {
            _companyProvider = companyProvider;
        }

        protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
        {
            var id = int.Parse(path.Groups[HistoryDefectiveRavMaterialId].Value);
            var response = await _companyProvider.GetOneHistoryDefectiveRavMaterialAsync(id);
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