using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Context.Providers.ProductInOnePackages;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Services.JwtToken;

namespace InventoryServer.Commands.ProductInOnePackage
{
    public class GetOneProductInOnePackage : AuthorizationCommand
    {
        private const string ProductInOnePackageId = "ProductInOnePackageId";

        public override string Path => @$"/ProductInOnePackage/GetOne?Id=(?<{ProductInOnePackageId}>.+)";

        public override HttpMethod Method => HttpMethod.Get;
        public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin, UserRole.WarehouseUser };
        private readonly IProductInOnePackageProvider _productInOnePackageProvider;

        public GetOneProductInOnePackage(IJwtTokenService jwtTokenService, IProductInOnePackageProvider productInOnePackageProvider) :
            base(jwtTokenService)
        {
            _productInOnePackageProvider = productInOnePackageProvider;
        }

        protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
        {
            var id = int.Parse(path.Groups[ProductInOnePackageId].Value);
            var response = await _productInOnePackageProvider.GetOneProductInOnePackageAsync(id);
            if (response is null)
            {
                await context.WriteResponseAsync(404, $"Записи под id: \"{id}\" не существует в базе данных")
                    .ConfigureAwait(false);
                return;
            }

            await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(response)).ConfigureAwait(false);
        }
    }
}