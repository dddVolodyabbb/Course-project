
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Context.Providers.ProductTypes;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Requests;
using InventoryServer.Services.JwtToken;

namespace InventoryServer.Commands.ProductType
{
    public class CreateProductType : AuthorizationCommand
    {
        public override string Path => @"/ProductType/Create";
        public override HttpMethod Method => HttpMethod.Post;
        public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };
        private readonly IProductTypeProvider _companyProvider;
        public CreateProductType(IJwtTokenService jwtTokenService, IProductTypeProvider companyProvider):base(jwtTokenService)
        {
            _companyProvider = companyProvider;
        }

        protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
        {
            var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
            if (!JsonSerializeHelper.TryDeserialize<ProductTypeRequest>(requestBody, out var productTypeRequest))
            {
                await context.WriteResponseAsync(400, "Недопустимое содержимое тела запроса").ConfigureAwait(false);
                return;
            }

            var productType = productTypeRequest.ToEntity();
            await _companyProvider.CreateProductTypeAsync(productType).ConfigureAwait(false);
            await context.WriteResponseAsync(201, null).ConfigureAwait(false);
        }
    }
}
