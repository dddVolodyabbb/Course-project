using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Context.Providers.ProductInOnePackages;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Requests;
using InventoryServer.Services.JwtToken;

namespace InventoryServer.Commands.ProductInOnePackage
{
    public class UpdateProductInOnePackage : AuthorizationCommand
    {
        private const string ProductInOnePackageId = "ProductInOnePackageId";
        public override string Path => @$"/ProductInOnePackage/Update?Id=(?<{ProductInOnePackageId}>.+)";
        public override HttpMethod Method => HttpMethod.Put;
        public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };
        private readonly IProductInOnePackageProvider _productInOnePackageProvider;

        public UpdateProductInOnePackage(IJwtTokenService jwtTokenService, IProductInOnePackageProvider productInOnePackageProvider) :
			base(jwtTokenService)
        {
            _productInOnePackageProvider = productInOnePackageProvider;
        }

        protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
        {
            var productInOnePackageId = int.Parse(path.Groups[ProductInOnePackageId].Value);
			var productInOnePackage = await _productInOnePackageProvider.GetOneProductInOnePackageAsync(productInOnePackageId);
            if (productInOnePackage is null)
            {
                await context
					.WriteResponseAsync(404, $"Записи под id: \"{productInOnePackageId}\" не существует в базе данных")
                    .ConfigureAwait(false);
                return;
            }

            var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
            if (!JsonSerializeHelper.TryDeserialize<ProductInOnePackageRequest>(requestBody, out var productInOnePackageRequest))
            {
                await context.WriteResponseAsync(400, "Недопустимое содержимое тела запроса").ConfigureAwait(false);
                return;
            }

            var newProductInOnePackage = productInOnePackageRequest.ToEntity().Result;
			await _productInOnePackageProvider
				.UpdateProductInOnePackageAsync(productInOnePackage.Id, newProductInOnePackage)
                .ConfigureAwait(false);
            await context.WriteResponseAsync(201, null).ConfigureAwait(false);
        }
    }
}