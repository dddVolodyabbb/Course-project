using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Context.Providers.PercentageOfRawMaterials;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Requests;
using InventoryServer.Services.JwtToken;

namespace InventoryServer.Commands.PercentageOfRawMaterial
{
    public class UpdatePercentageOfRawMaterial : AuthorizationCommand
    {
        private const string PercentageOfRawMaterialId = "PercentageOfRawMaterialId";
        public override string Path => @$"/PercentageOfRawMaterial/Update?Id=(?<{PercentageOfRawMaterialId}>.+)";
        public override HttpMethod Method => HttpMethod.Put;
        public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };
        private readonly IPercentageOfRawMaterialProvider _percentageOfRawMaterialProvider;

        public UpdatePercentageOfRawMaterial(IJwtTokenService jwtTokenService, IPercentageOfRawMaterialProvider percentageOfRawMaterialProvider) :
			base(jwtTokenService)
        {
            _percentageOfRawMaterialProvider = percentageOfRawMaterialProvider;
        }

        protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
        {
            var percentageOfRawMaterialId = int.Parse(path.Groups[PercentageOfRawMaterialId].Value);
			var percentageOfRawMaterial = await _percentageOfRawMaterialProvider.GetOnePercentageOfRawMaterialAsync(percentageOfRawMaterialId);
            if (percentageOfRawMaterial is null)
            {
                await context
					.WriteResponseAsync(404, $"Записи под id: \"{percentageOfRawMaterialId}\" не существует в базе данных")
                    .ConfigureAwait(false);
                return;
            }

            var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
            if (!JsonSerializeHelper.TryDeserialize<PercentageOfRawMaterialRequest>(requestBody, out var percentageOfRawMaterialRequest))
            {
                await context.WriteResponseAsync(400, "Недопустимое содержимое тела запроса").ConfigureAwait(false);
                return;
            }

            var newPercentageOfRawMaterial = percentageOfRawMaterialRequest.ToEntity().Result;
			await _percentageOfRawMaterialProvider
				.UpdatePercentageOfRawMaterialAsync(percentageOfRawMaterial.Id, newPercentageOfRawMaterial)
                .ConfigureAwait(false);
            await context.WriteResponseAsync(201, null).ConfigureAwait(false);
        }
    }
}