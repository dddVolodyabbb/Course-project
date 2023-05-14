using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Commands;
using InventoryServer.Context.Providers.DeliveryCompanies;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Services.JwtToken;

namespace Inventory_server.Commands.DeliveryCompany
{
    public class DeleteDeliveryCompany : AuthorizationCommand
    {
        private const string DeliveryCompanyName = "DeliveryCompanyName";
        public override string Path => @$"/DeliveryCompany/Delete?Name=(?<{DeliveryCompanyName}>.+)";
        public override HttpMethod Method => HttpMethod.Delete;
        public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };
        private readonly IDeliveryCompanyProvider _companyProvider;
        public DeleteDeliveryCompany(IJwtTokenService jwtTokenService, IDeliveryCompanyProvider companyProvider) : base(jwtTokenService)
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

            await _companyProvider.DeleteDeliveryCompanyAsync(deliveryCompany).ConfigureAwait(false);
            await context.WriteResponseAsync(201, null).ConfigureAwait(false);
        }
    }
}
