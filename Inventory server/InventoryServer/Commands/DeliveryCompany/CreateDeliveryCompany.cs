
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Context.Providers.DeliveryCompanies;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Requests;
using InventoryServer.Services.JwtToken;

namespace InventoryServer.Commands.DeliveryCompany
{
    public class CreateDeliveryCompany : AuthorizationCommand
    {
        public override string Path => @"/DeliveryCompany/Create";
        public override HttpMethod Method => HttpMethod.Post;
        public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };
        private readonly IDeliveryCompanyProvider _deliveryCompany;
        public CreateDeliveryCompany(IJwtTokenService jwtTokenService, IDeliveryCompanyProvider deliveryCompany) :base(jwtTokenService)
        {
            _deliveryCompany = deliveryCompany;
        }

        protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
        {
            var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);

            if (!JsonSerializeHelper.TryDeserialize<DeliveryCompanyRequest>(requestBody, out var deliveryCompanyRequest))
            {
                await context.WriteResponseAsync(400, "Не правельное тело запроса").ConfigureAwait(false);
                return;
            }
            var deliveryCompany = deliveryCompanyRequest.ToEntity();
            await _deliveryCompany.CreateDeliveryCompanyAsync(deliveryCompany).ConfigureAwait(false);
            await context.WriteResponseAsync(201, null).ConfigureAwait(false);
        }
    }
}
