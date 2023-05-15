
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Commands;
using InventoryServer.Context.Providers.ProductTypes;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Services.JwtToken;

namespace Inventory_server.Commands.ProductType
{
    public class DeleteProductType : AuthorizationCommand
    {
        private const string ProductTypeId = "ProductTypeId";
        public override string Path => @$"/ProductType/Delete?Id=(?<{ProductTypeId}>.+)";
        public override HttpMethod Method => HttpMethod.Delete;
        public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };
        private readonly IProductTypeProvider _companyProvider;
        public DeleteProductType(IJwtTokenService jwtTokenService, IProductTypeProvider companyProvider) : base(jwtTokenService)
        {
            _companyProvider = companyProvider;
        }

        protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
        {
            var productTypeId = int.Parse(path.Groups[ProductTypeId].Value);
			var productType = await _companyProvider.GetOneProductTypeAsync(productTypeId);
            if (productType is null)
            {
                await context.WriteResponseAsync(404, $"Компании \"{ProductTypeId}\" не существует в базе данных").ConfigureAwait(false);
                return;
            }

            await _companyProvider.DeleteProductTypeAsync(productType.Id).ConfigureAwait(false);
            await context.WriteResponseAsync(201, null).ConfigureAwait(false);
        }
    }
}
