using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Context.Providers.HistoryDefectiveProducts;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Services.JwtToken;

namespace InventoryServer.Commands.HistoryDefectiveProduct
{
    public class GetOneHistoryDefectiveProduct : AuthorizationCommand
    {
        private const string HistoryDefectiveProductId = "HistoryDefectiveProductId";
        public override string Path => @$"/HistoryDefectiveProduct/GetOne?Id=(?<{HistoryDefectiveProductId}>.+)";
        public override HttpMethod Method => HttpMethod.Get;
        public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin, UserRole.WarehouseUser };
        private readonly IHistoryDefectiveProductProvider _historyDefectiveProductProvider;

        public GetOneHistoryDefectiveProduct(IJwtTokenService jwtTokenService, IHistoryDefectiveProductProvider historyDefectiveProductProvider) :
            base(jwtTokenService)
        {
            _historyDefectiveProductProvider = historyDefectiveProductProvider;
        }

        protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
        {
            var id = int.Parse(path.Groups[HistoryDefectiveProductId].Value);
            var response = await _historyDefectiveProductProvider.GetOneHistoryDefectiveProductAsync(id);
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