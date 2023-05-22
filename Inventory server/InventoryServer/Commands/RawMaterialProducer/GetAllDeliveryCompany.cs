
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using InventoryServer.Commands;
using InventoryServer.Context.Providers.RawMaterialProducers;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Services.JwtToken;

namespace Inventory_server.Commands.RawMaterialProducer
{
    public class GetAllRawMaterialProducer : AuthorizationCommand
    {
        public override string Path => @"/RawMaterialProducer/GetAll";
        public override HttpMethod Method => HttpMethod.Get;
        public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin, UserRole.WarehouseUser};
        private readonly IRawMaterialProducerProvider _companyProvider;
        public GetAllRawMaterialProducer(IJwtTokenService jwtTokenService, IRawMaterialProducerProvider companyProvider) : base(jwtTokenService)
        {
            _companyProvider = companyProvider;
        }

        protected override async Task HandleRequestInternalAsync(HttpListenerContext context)
        {
            var rawMaterialProducerCollection = await _companyProvider.GetAllRawMaterialProducerAsync();
            var response = rawMaterialProducerCollection.Select(rawMaterialProducer => rawMaterialProducer.ToResponse()).ToList();

            await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(response)).ConfigureAwait(false);
        }
    }
}
