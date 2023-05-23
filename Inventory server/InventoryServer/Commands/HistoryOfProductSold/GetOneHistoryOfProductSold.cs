using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Context.Providers.HistoryOfProductSolids;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Services.JwtToken;

namespace InventoryServer.Commands.HistoryOfProductSold
{
    public class GetOneHistoryOfProductSold : AuthorizationCommand
    {
        private const string HistoryOfProductSoldId = "HistoryOfProductSoldId";
		public override string Path => @$"/HistoryOfProductSold/GetOne?Id=(?<{HistoryOfProductSoldId}>.+)";
		public override HttpMethod Method => HttpMethod.Get;
        public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin, UserRole.WarehouseUser };
        private readonly IHistoryOfProductSoldProvider _historyOfProductSoldProvider;

        public GetOneHistoryOfProductSold(IJwtTokenService jwtTokenService, IHistoryOfProductSoldProvider historyOfProductSoldProvider) :
            base(jwtTokenService)
        {
            _historyOfProductSoldProvider = historyOfProductSoldProvider;
        }

        protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
        {
            var id = int.Parse(path.Groups[HistoryOfProductSoldId].Value);
            var response = await _historyOfProductSoldProvider.GetOneHistoryOfProductSoldAsync(id);
            if (response is null)
            {
                await context.WriteResponseAsync(404, $"Записи под id: \"{id}\" не существует в базе данных")
                    .ConfigureAwait(false);
                return;
            }

            await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(response.ToResponse())).ConfigureAwait(false);
        }
    }
}