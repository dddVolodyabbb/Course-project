
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
        private const string DeliveryCompanyName = "DeliveryCompanyName";
        public override string Path => @$"/DeliveryCompany/Create?Name=(?<{DeliveryCompanyName}>.+)";
        public override HttpMethod Method => HttpMethod.Post;
        public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };
        private readonly IDeliveryCompanyProvider _companyProvider;
        public CreateDeliveryCompany(IJwtTokenService jwtTokenService, IDeliveryCompanyProvider companyProvider):base(jwtTokenService)
        {
            _companyProvider = companyProvider;
        }

        protected override async Task HandleRequestInternalAsync(HttpListenerContext context)
        {
            var match = Regex.Match(context.Request.Url.AbsolutePath, Path, RegexOptions.IgnoreCase);
            var deliveryCompanyName = match.Groups[DeliveryCompanyName].Value;
            if (deliveryCompanyName is "" or "DeliveryCompanyName")
                await context.WriteResponseAsync(404, "Не введенно название компании").ConfigureAwait(false);

            var deliveryCompany = new DeliveryCompanyRequest
                {Name = DeliveryCompanyName}.ToEntity();

            await _companyProvider.CreateDeliveryCompanyAsync(deliveryCompany).ConfigureAwait(false);
            await context.WriteResponseAsync(201, null).ConfigureAwait(false);
        }
    }
}
