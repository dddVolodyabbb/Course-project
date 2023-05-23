
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Commands;
using InventoryServer.Context.Providers.RawMaterialProducers;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Services.JwtToken;

namespace Inventory_server.Commands.RawMaterialProducer
{
    public class GetOneRawMaterialProducer : AuthorizationCommand
    {
        private const string RawMaterialProducerName = "RawMaterialProducerName";
        public override string Path => @$"/RawMaterialProducer/GetOne?Name=(?<{RawMaterialProducerName}>.+)";
        public override HttpMethod Method => HttpMethod.Get;
        public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin, UserRole.WarehouseUser };
        private readonly IRawMaterialProducerProvider _companyProvider;

        public GetOneRawMaterialProducer(IJwtTokenService jwtTokenService, IRawMaterialProducerProvider companyProvider) : base(
            jwtTokenService)
        {
            _companyProvider = companyProvider;
        }

        protected override async Task HandleRequestInternalAsync(HttpListenerContext context)
        {
            var match = Regex.Match(context.Request.Url.AbsolutePath, Path, RegexOptions.IgnoreCase);
            var rawMaterialProducerName = match.Groups[RawMaterialProducerName].Value;
            if (rawMaterialProducerName is "" or "RawMaterialProducerName")
                await context.WriteResponseAsync(404, "Не введенно название компании").ConfigureAwait(false);
            var response = await _companyProvider.GetOneRawMaterialProducerAsync(rawMaterialProducerName);

            await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(response)).ConfigureAwait(false);
        }
    }
}