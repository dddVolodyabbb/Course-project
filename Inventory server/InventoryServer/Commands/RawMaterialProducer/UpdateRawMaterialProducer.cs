using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Commands;
using InventoryServer.Context.Contexts;
using InventoryServer.Context.Providers.RawMaterialProducers;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Requests;
using InventoryServer.Services.JwtToken;

namespace Inventory_server.Commands.RawMaterialProducer
{
    public class UpdateRawMaterialProducer : AuthorizationCommand
    {
        private const string RawMaterialProducerName = "RawMaterialProducerName";
        public override string Path => @$"/RawMaterialProducer/Update?Name=(?<{RawMaterialProducerName}>.+)";
        public override HttpMethod Method => HttpMethod.Put;
        public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };
        private readonly IRawMaterialProducerProvider _companyProvider;
        public UpdateRawMaterialProducer(IJwtTokenService jwtTokenService, IRawMaterialProducerProvider companyProvider) : base(jwtTokenService)
        {
            _companyProvider = companyProvider;
        }

        protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
        {
            var rawMaterialProducerName = path.Groups[RawMaterialProducerName].Value;
            if (rawMaterialProducerName is "" or "RawMaterialProducerName")
                await context.WriteResponseAsync(404, "Не введенно название компании").ConfigureAwait(false);

            var rawMaterialProducer = await _companyProvider.GetOneRawMaterialProducerAsync(rawMaterialProducerName);
            if (rawMaterialProducer is null)
            {
                await context.WriteResponseAsync(404, $"Компании \"{rawMaterialProducerName}\" не существует в базе данных").ConfigureAwait(false);
                return;
            }

            var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
            if (!JsonSerializeHelper.TryDeserialize<RawMaterialProducerRequest>(requestBody, out var rawMaterialProducerRequest))
            {
                await context.WriteResponseAsync(400, "Недопустимое содержимое тела запроса").ConfigureAwait(false);
                return;
            }

            var newRawMaterialProducer = rawMaterialProducerRequest.ToEntity();

            await _companyProvider.UpdateRawMaterialProducerAsync(rawMaterialProducer, newRawMaterialProducer).ConfigureAwait(false);
            await context.WriteResponseAsync(201, null).ConfigureAwait(false);
        }
    }
}
