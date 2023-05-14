
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Commands;
using InventoryServer.Context.Providers;
using InventoryServer.Context.Providers.RawMaterialTypes;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Services.JwtToken;

namespace Inventory_server.Commands.RawMaterialType
{
    public class DeleteRawMaterialType : AuthorizationCommand
    {
        private const string RawMaterialTypeName = "RawMaterialTypeName";
        public override string Path => @$"/RawMaterialType/Delete?Name=(?<{RawMaterialTypeName}>.+)";
        public override HttpMethod Method => HttpMethod.Delete;
        public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };
        private readonly IRawMaterialTypeProvider _companyProvider;
        public DeleteRawMaterialType(IJwtTokenService jwtTokenService, IRawMaterialTypeProvider companyProvider) : base(jwtTokenService)
        {
            _companyProvider = companyProvider;
        }

        protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
        {
            var rawMaterialTypeName = path.Groups[RawMaterialTypeName].Value;
            if (rawMaterialTypeName is "" or "RawMaterialTypeName")
                await context.WriteResponseAsync(404, "Не введенно название компании").ConfigureAwait(false);

            var rawMaterialType = await _companyProvider.GetOneRawMaterialTypeAsync(rawMaterialTypeName);
            if (rawMaterialType is null)
            {
                await context.WriteResponseAsync(404, $"Компании \"{rawMaterialTypeName}\" не существует в базе данных").ConfigureAwait(false);
                return;
            }

            await _companyProvider.DeleteRawMaterialTypeAsync(rawMaterialType).ConfigureAwait(false);
            await context.WriteResponseAsync(201, null).ConfigureAwait(false);
        }
    }
}
