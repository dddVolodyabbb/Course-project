using System.Linq;
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
    public class GetAllHistoryDefectiveProduct : AuthorizationCommand
    {
        public override string Path => @"/HistoryDefectiveProduct/GetAll";
        public override HttpMethod Method => HttpMethod.Get;
        public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin, UserRole.WarehouseUser };
        private readonly IHistoryDefectiveProductProvider _historyDefectiveProductProvider;

        public GetAllHistoryDefectiveProduct(IJwtTokenService jwtTokenService, IHistoryDefectiveProductProvider historyDefectiveProductProvider) :
			base(jwtTokenService)
        {
            _historyDefectiveProductProvider = historyDefectiveProductProvider;
        }

        protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
        {
            var historyDefectiveProductCollection = await _historyDefectiveProductProvider.GetAllHistoryDefectiveProductAsync();
            var response = historyDefectiveProductCollection
                .Select(historyDefectiveProduct => historyDefectiveProduct.ToResponse()).ToList();
			await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(response)).ConfigureAwait(false);
        }
    }
}