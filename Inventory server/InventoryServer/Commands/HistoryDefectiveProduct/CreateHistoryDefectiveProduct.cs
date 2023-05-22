using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Context.Providers.HistoryDefectiveProducts;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Requests;
using InventoryServer.Services.JwtToken;

namespace InventoryServer.Commands.HistoryDefectiveProduct
{
    public class CreateHistoryDefectiveProduct : AuthorizationCommand
    {
        public override string Path => @"/HistoryDefectiveProduct/Create";
        public override HttpMethod Method => HttpMethod.Post;
        public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };
        private readonly IHistoryDefectiveProductProvider _historyDefectiveProductProvider;

        public CreateHistoryDefectiveProduct(IJwtTokenService jwtTokenService, IHistoryDefectiveProductProvider historyDefectiveProductProvider) :
			base(jwtTokenService)
        {
            _historyDefectiveProductProvider = historyDefectiveProductProvider;
        }

        protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
        {
            var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
            if (!JsonSerializeHelper.TryDeserialize<HistoryDefectiveProductRequest>(requestBody,
                    out var historyDefectiveProductRequest))
            {
                await context.WriteResponseAsync(400, "Недопустимое содержимое тела запроса").ConfigureAwait(false);
                return;
            }

            var historyDefectiveProduct = await historyDefectiveProductRequest.ToEntity().ConfigureAwait(false);
            await _historyDefectiveProductProvider.CreateHistoryDefectiveProductAsync(historyDefectiveProduct).ConfigureAwait(false);
            await context.WriteResponseAsync(201, null).ConfigureAwait(false);
        }
    }
}