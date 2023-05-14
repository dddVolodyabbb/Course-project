
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Commands;
using InventoryServer.Context.Providers.DeliveryCompanies;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Requests;
using InventoryServer.Services.JwtToken;

namespace Inventory_server.Commands.DeliveryCompany
{
    public class UpdateDeliveryCompany : AuthorizationCommand
    {
        private const string DeliveryCompanyName = "DeliveryCompanyName";
        public override string Path => @$"/DeliveryCompany/Update?Name=(?<{DeliveryCompanyName}>.+)";
        public override HttpMethod Method => HttpMethod.Put;
        public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };
        private readonly IDeliveryCompanyProvider _companyProvider;
        public UpdateDeliveryCompany(IJwtTokenService jwtTokenService, IDeliveryCompanyProvider companyProvider) : base(jwtTokenService)
        {
            _companyProvider = companyProvider;
        }

        protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
        {
            var deliveryCompanyName = path.Groups[DeliveryCompanyName].Value;
            if (deliveryCompanyName is "" or "DeliveryCompanyName")
                await context.WriteResponseAsync(404, "Не введенно название компании").ConfigureAwait(false);

            var deliveryCompany = await _companyProvider.GetOneDeliveryCompanyAsync(deliveryCompanyName);
            if (deliveryCompany is null)
            {
                await context.WriteResponseAsync(404, $"Компании \"{deliveryCompanyName}\" не существует в базе данных").ConfigureAwait(false);
                return;
            }

            var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
            if (!JsonSerializeHelper.TryDeserialize<DeliveryCompanyRequest>(requestBody, out var deliveryCompanyRequest))
            {
                await context.WriteResponseAsync(400, "Недопустимое содержимое тела запроса").ConfigureAwait(false);
                return;
            }

            var newDeliveryCompany = deliveryCompanyRequest.ToEntity();

            await _companyProvider.UpdateDeliveryCompanyAsync(deliveryCompany, newDeliveryCompany).ConfigureAwait(false);
            await context.WriteResponseAsync(201, null).ConfigureAwait(false);
        }
    }
}
