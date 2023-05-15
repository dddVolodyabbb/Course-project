
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
        private const string ProductTypeId = "ProductTypeId";
        public override string Path => @$"/ProductType/Update?Id=(?<{ProductTypeId}>.+)";
        public override HttpMethod Method => HttpMethod.Put;
        public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };
        private readonly IProductTypeProvider _companyProvider;
        public UpdateProductType(IJwtTokenService jwtTokenService, IProductTypeProvider companyProvider) : 
			base(jwtTokenService)
        {
            _companyProvider = companyProvider;
        }

        protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
        {
            var productTypeId = int.Parse(path.Groups[ProductTypeId].Value);
			var productType = await _companyProvider.GetOneProductTypeAsync(productTypeId);
            if (productType is null)
            {
                await context.WriteResponseAsync(404, $"Компании \"{productTypeId}\" не существует в базе данных").ConfigureAwait(false);
                return;
            }

            var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
            if (!JsonSerializeHelper.TryDeserialize<ProductTypeRequest>(requestBody, out var productTypeRequest))
            {
                await context.WriteResponseAsync(400, "Недопустимое содержимое тела запроса").ConfigureAwait(false);
                return;
            }

            var newProductType = productTypeRequest.ToEntity();
			await _companyProvider.UpdateProductTypeAsync(productType.Id, newProductType).ConfigureAwait(false);
            await context.WriteResponseAsync(201, null).ConfigureAwait(false);
        }
    }
}
