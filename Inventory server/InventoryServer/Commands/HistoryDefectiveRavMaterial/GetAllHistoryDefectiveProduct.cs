using System.Linq;
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
    public class GetAllHistoryDefectiveRavMaterial : AuthorizationCommand
    {
        public override string Path => @"/HistoryDefectiveRavMaterial/GetAll";
        public override HttpMethod Method => HttpMethod.Get;
        public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin, UserRole.WarehouseUser };
        private readonly IHistoryDefectiveRavMaterialProvider _companyProvider;

        public GetAllHistoryDefectiveRavMaterial(IJwtTokenService jwtTokenService,
            IHistoryDefectiveRavMaterialProvider companyProvider) : base(jwtTokenService)
        {
            _companyProvider = companyProvider;
        }

        protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
        {
            var historyDefectiveRavMaterialCollection = await _companyProvider.GetAllHistoryDefectiveRavMaterialAsync();
            var response = historyDefectiveRavMaterialCollection
                .Select(historyDefectiveRavMaterial => historyDefectiveRavMaterial.ToResponse()).ToList();

            await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(response)).ConfigureAwait(false);
        }
    }
}