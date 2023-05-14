using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Context.Providers.RawMaterialProducers;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Requests;
using InventoryServer.Services.JwtToken;

namespace InventoryServer.Commands.RawMaterialProducer
{
    public class CreateRawMaterialProducer : AuthorizationCommand
    {

        public override string Path => @"/RawMaterialProducer/Create";
        public override HttpMethod Method => HttpMethod.Post;
        public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };
        private readonly IRawMaterialProducerProvider _companyProvider;
        public CreateRawMaterialProducer(IJwtTokenService jwtTokenService, IRawMaterialProducerProvider companyProvider):base(jwtTokenService)
        {
            _companyProvider = companyProvider;
        }

        protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
        {
            var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
            if (!JsonSerializeHelper.TryDeserialize<RawMaterialProducerRequest>(requestBody, out var rawMaterialProducerRequest))
            {
                await context.WriteResponseAsync(400, "Не правельное тело запроса").ConfigureAwait(false);
                return;
            }

            var rawMaterialProducer = rawMaterialProducerRequest.ToEntity();
            await _companyProvider.CreateRawMaterialProducerAsync(rawMaterialProducer).ConfigureAwait(false);
            await context.WriteResponseAsync(201, null).ConfigureAwait(false);
        }
    }
}
