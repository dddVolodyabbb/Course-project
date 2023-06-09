﻿using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Context.Providers.HistoryOfProductSolids;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Requests;
using InventoryServer.Services.JwtToken;

namespace InventoryServer.Commands.HistoryOfProductSold
{
    public class UpdateHistoryOfProductSold : AuthorizationCommand
    {
        private const string HistoryOfProductSoldId = "HistoryOfProductSoldId";
        public override string Path => @$"/HistoryOfProductSold/Update?Id=(?<{HistoryOfProductSoldId}>.+)";
        public override HttpMethod Method => HttpMethod.Put;
        public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };
        private readonly IHistoryOfProductSoldProvider _historyOfProductSoldProvider;

        public UpdateHistoryOfProductSold(IJwtTokenService jwtTokenService, IHistoryOfProductSoldProvider historyOfProductSoldProvider) :
			base(jwtTokenService)
        {
            _historyOfProductSoldProvider = historyOfProductSoldProvider;
        }

        protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
        {
            var historyOfProductSoldId = int.Parse(path.Groups[HistoryOfProductSoldId].Value);
			var historyOfProductSold = await _historyOfProductSoldProvider.GetOneHistoryOfProductSoldAsync(historyOfProductSoldId);
            if (historyOfProductSold is null)
            {
                await context
					.WriteResponseAsync(404, $"Записи под id: \"{historyOfProductSoldId}\" не существует в базе данных")
                    .ConfigureAwait(false);
                return;
            }

            var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
            if (!JsonSerializeHelper.TryDeserialize<HistoryOfProductSoldRequest>(requestBody, out var historyOfProductSoldRequest))
            {
                await context.WriteResponseAsync(400, "Недопустимое содержимое тела запроса").ConfigureAwait(false);
                return;
            }

            var newHistoryOfProductSold = historyOfProductSoldRequest.ToEntity().Result;
			await _historyOfProductSoldProvider
				.UpdateHistoryOfProductSoldAsync(historyOfProductSold.Id, newHistoryOfProductSold)
                .ConfigureAwait(false);
            await context.WriteResponseAsync(201, null).ConfigureAwait(false);
        }
    }
}