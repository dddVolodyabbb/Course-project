
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using InventoryServer.Commands;
using InventoryServer.Context.Providers.ProductTypes;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Services.JwtToken;

namespace Inventory_server.Commands.ProductType
{
    public class GetAllProductType : AuthorizationCommand
    {
        public override string Path => @"/ProductType/GetAll";
        public override HttpMethod Method => HttpMethod.Get;
        public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin, UserRole.WarehouseUser};
        private readonly IProductTypeProvider _companyProvider;
        public GetAllProductType(IJwtTokenService jwtTokenService, IProductTypeProvider companyProvider) : base(jwtTokenService)
        {
            _companyProvider = companyProvider;
        }

        protected override async Task HandleRequestInternalAsync(HttpListenerContext context)
        {
            var productTypeCollection = await _companyProvider.GetAllProductTypeAsync();
            var response = productTypeCollection.Select(productType => productType.ToResponse()).ToList();

            await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(response)).ConfigureAwait(false);
        }
    }
}
