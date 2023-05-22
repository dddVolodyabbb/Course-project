using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Context.Providers.HistoryDefectiveRavMaterials;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Services.JwtToken;

namespace InventoryServer.Commands.HistoryDefectiveRavMaterial
{
    public class GetAllHistoryDefectiveRavMaterial : AuthorizationCommand
    {
        public override string Path => @"/HistoryDefectiveRavMaterial/GetAll";
        public override HttpMethod Method => HttpMethod.Get;
        public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin, UserRole.WarehouseUser };
        private readonly IHistoryDefectiveRavMaterialProvider _historyDefectiveRavMaterialProvider;

        public GetAllHistoryDefectiveRavMaterial(IJwtTokenService jwtTokenService, IHistoryDefectiveRavMaterialProvider historyDefectiveRavMaterialProvider) :
			base(jwtTokenService)
        {
            _historyDefectiveRavMaterialProvider = historyDefectiveRavMaterialProvider;
        }

        protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
        {
            var historyDefectiveRavMaterialCollection = await _historyDefectiveRavMaterialProvider.GetAllHistoryDefectiveRavMaterialAsync();
            var response = historyDefectiveRavMaterialCollection
                .Select(historyDefectiveRavMaterial => historyDefectiveRavMaterial.ToResponse()).ToList();
			await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(response)).ConfigureAwait(false);
        }
    }
}