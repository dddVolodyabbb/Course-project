
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Commands;
using InventoryServer.Context.Providers.ProductTypes;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Requests;
using InventoryServer.Services.JwtToken;

namespace Inventory_server.Commands.ProductType
{
    public class UpdateProductType : AuthorizationCommand
    {
        private const string ProductTypeName = "ProductTypeName";
        public override string Path => @$"/ProductType/Update?Name=(?<{ProductTypeName}>.+)";
        public override HttpMethod Method => HttpMethod.Put;
        public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };
        private readonly IProductTypeProvider _companyProvider;
        public UpdateProductType(IJwtTokenService jwtTokenService, IProductTypeProvider companyProvider) : base(jwtTokenService)
        {
            _companyProvider = companyProvider;
        }

        protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
        {
            var productTypeName = path.Groups[ProductTypeName].Value;
            if (productTypeName is "" or "ProductTypeName")
                await context.WriteResponseAsync(404, "Не введенно название компании").ConfigureAwait(false);

            var productType = await _companyProvider.GetOneProductTypeAsync(productTypeName);
            if (productType is null)
            {
                await context.WriteResponseAsync(404, $"Компании \"{productTypeName}\" не существует в базе данных").ConfigureAwait(false);
                return;
            }

            var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
            if (!JsonSerializeHelper.TryDeserialize<ProductTypeRequest>(requestBody, out var ProductTypeRequest))
            {
                await context.WriteResponseAsync(400, "Недопустимое содержимое тела запроса").ConfigureAwait(false);
                return;
            }

            var newProductType = ProductTypeRequest.ToEntity();

            await _companyProvider.UpdateProductTypeAsync(productType, newProductType).ConfigureAwait(false);
            await context.WriteResponseAsync(201, null).ConfigureAwait(false);
        }
    }
}
