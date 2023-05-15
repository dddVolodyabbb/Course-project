using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Commands;
using InventoryServer.Context.Providers.HistoryDefectiveProducts;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Services.JwtToken;

namespace Inventory_server.Commands.HistoryDefectiveProduct
{
    public class GetOneHistoryDefectiveProduct : AuthorizationCommand
    {
        private const string HistoryDefectiveProductId = "HistoryDefectiveProductId";
        public override string Path => @$"/HistoryDefectiveProduct/GetOne?Id=(?<{HistoryDefectiveProductId}>.+)";
        public override HttpMethod Method => HttpMethod.Get;
        public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin, UserRole.WarehouseUser };
        private readonly IHistoryDefectiveProductProvider _companyProvider;

        public GetOneHistoryDefectiveProduct(IJwtTokenService jwtTokenService,
            IHistoryDefectiveProductProvider companyProvider) :
            base(jwtTokenService)
        {
            _companyProvider = companyProvider;
        }

        protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
        {
            var id = int.Parse(path.Groups[HistoryDefectiveProductId].Value);
            var response = await _companyProvider.GetOneHistoryDefectiveProductAsync(id);
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