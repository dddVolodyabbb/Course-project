using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Context.Providers.RawMaterialInOnePackages;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Requests;
using InventoryServer.Services.JwtToken;

namespace InventoryServer.Commands.RawMaterialInOnePackage
{
    public class UpdateRawMaterialInOnePackage : AuthorizationCommand
    {
        private const string RawMaterialInOnePackageId = "RawMaterialInOnePackageId";
        public override string Path => @$"/RawMaterialInOnePackage/Update?Id=(?<{RawMaterialInOnePackageId}>.+)";
        public override HttpMethod Method => HttpMethod.Put;
        public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };
        private readonly IRawMaterialInOnePackageProvider _rawMaterialInOnePackageProvider;

        public UpdateRawMaterialInOnePackage(IJwtTokenService jwtTokenService, IRawMaterialInOnePackageProvider rawMaterialInOnePackageProvider) : 
			base(jwtTokenService)
        {
            _rawMaterialInOnePackageProvider = rawMaterialInOnePackageProvider;
        }

        protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
        {
            var rawMaterialInOnePackageId = int.Parse(path.Groups[RawMaterialInOnePackageId].Value);
			var rawMaterialInOnePackage = await _rawMaterialInOnePackageProvider.GetOneRawMaterialInOnePackageAsync(rawMaterialInOnePackageId);
            if (rawMaterialInOnePackage is null)
            {
                await context
					.WriteResponseAsync(404, $"Записи под id: \"{rawMaterialInOnePackageId}\" не существует в базе данных")
                    .ConfigureAwait(false);
                return;
            }

            var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
            if (!JsonSerializeHelper.TryDeserialize<RawMaterialInOnePackageRequest>(requestBody, out var rawMaterialInOnePackageRequest))
            {
                await context.WriteResponseAsync(400, "Недопустимое содержимое тела запроса").ConfigureAwait(false);
                return;
            }

            var newRawMaterialInOnePackage = rawMaterialInOnePackageRequest.ToEntity().Result;
			await _rawMaterialInOnePackageProvider
				.UpdateRawMaterialInOnePackageAsync(rawMaterialInOnePackage.Id, newRawMaterialInOnePackage)
                .ConfigureAwait(false);
            await context.WriteResponseAsync(201, null).ConfigureAwait(false);
        }
    }
}