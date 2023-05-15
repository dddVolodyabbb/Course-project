using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Context.Providers.HistoryDefectiveRavMaterials;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Requests;
using InventoryServer.Services.JwtToken;

namespace InventoryServer.Commands.HistoryDefectiveRavMaterial
{
    public class CreateHistoryDefectiveRavMaterial : AuthorizationCommand
    {
        public override string Path => @"/HistoryDefectiveRavMaterial/Create";
        public override HttpMethod Method => HttpMethod.Post;
        public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };
        private readonly IHistoryDefectiveRavMaterialProvider _companyProvider;

        public CreateHistoryDefectiveRavMaterial(IJwtTokenService jwtTokenService,
            IHistoryDefectiveRavMaterialProvider companyProvider) : base(jwtTokenService)
        {
            _companyProvider = companyProvider;
        }

        protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
        {
            var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
            if (!JsonSerializeHelper.TryDeserialize<HistoryDefectiveRavMaterialRequest>(requestBody,
                    out var historyDefectiveRavMaterialRequest))
            {
                await context.WriteResponseAsync(400, "Недопустимое содержимое тела запроса").ConfigureAwait(false);
                return;
            }

            var historyDefectiveRavMaterial = await historyDefectiveRavMaterialRequest.ToEntity().ConfigureAwait(false);
            await _companyProvider.CreateHistoryDefectiveRavMaterialAsync(historyDefectiveRavMaterial)
                .ConfigureAwait(false);
            await context.WriteResponseAsync(201, null).ConfigureAwait(false);
        }
    }
}