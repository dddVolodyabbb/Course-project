using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Context.Providers.RawMaterialInOnePackages;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Services.JwtToken;

namespace InventoryServer.Commands.RawMaterialInOnePackage
{
    public class GetOneRawMaterialInOnePackage : AuthorizationCommand
    {
        private const string RawMaterialInOnePackageId = "RawMaterialInOnePackageId";
		public override string Path => @$"/RawMaterialInOnePackage/GetOne?Id=(?<{RawMaterialInOnePackageId}>.+)";
		public override HttpMethod Method => HttpMethod.Get;
        public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin, UserRole.WarehouseUser };
        private readonly IRawMaterialInOnePackageProvider _rawMaterialInOnePackageProvider;

        public GetOneRawMaterialInOnePackage(IJwtTokenService jwtTokenService, IRawMaterialInOnePackageProvider rawMaterialInOnePackageProvider) :
            base(jwtTokenService)
        {
            _rawMaterialInOnePackageProvider = rawMaterialInOnePackageProvider;
        }

        protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
        {
            var id = int.Parse(path.Groups[RawMaterialInOnePackageId].Value);
            var response = await _rawMaterialInOnePackageProvider.GetOneRawMaterialInOnePackageAsync(id);
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