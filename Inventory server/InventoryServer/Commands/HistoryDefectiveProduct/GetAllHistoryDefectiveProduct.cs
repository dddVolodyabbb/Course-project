using System.Linq;
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
    public class GetAllHistoryDefectiveProduct : AuthorizationCommand
    {
        public override string Path => @"/HistoryDefectiveProduct/GetAll";
        public override HttpMethod Method => HttpMethod.Get;
        public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin, UserRole.WarehouseUser };
        private readonly IHistoryDefectiveProductProvider _companyProvider;

        public GetAllHistoryDefectiveProduct(IJwtTokenService jwtTokenService,
            IHistoryDefectiveProductProvider companyProvider) : base(jwtTokenService)
        {
            _companyProvider = companyProvider;
        }

        protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
        {
            var historyDefectiveProductCollection = await _companyProvider.GetAllHistoryDefectiveProductAsync();
            var response = historyDefectiveProductCollection
                .Select(historyDefectiveProduct => historyDefectiveProduct.ToResponse()).ToList();

            await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(response)).ConfigureAwait(false);
        }
    }
}