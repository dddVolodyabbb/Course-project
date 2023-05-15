﻿using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Commands;
using InventoryServer.Context.Providers.HistoryDefectiveRavMaterials;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Requests;
using InventoryServer.Services.JwtToken;

namespace Inventory_server.Commands.HistoryDefectiveRavMaterial
{
	public class UpdateHistoryDefectiveRavMaterial : AuthorizationCommand
	{
		private const string HistoryDefectiveRavMaterialId = "HistoryDefectiveRavMaterialId";
		public override string Path => @$"/HistoryDefectiveRavMaterial/Update?Id=(?<{HistoryDefectiveRavMaterialId}>.+)";
		public override HttpMethod Method => HttpMethod.Put;
		public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };
		private readonly IHistoryDefectiveRavMaterialProvider _companyProvider;

		public UpdateHistoryDefectiveRavMaterial(IJwtTokenService jwtTokenService, IHistoryDefectiveRavMaterialProvider companyProvider) : 
			base(jwtTokenService)
		{
			_companyProvider = companyProvider;
		}

		protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
		{
			var historyDefectiveRavMaterialId = int.Parse(path.Groups[HistoryDefectiveRavMaterialId].Value);
			var historyDefectiveRavMaterial = await _companyProvider.GetOneHistoryDefectiveRavMaterialAsync(historyDefectiveRavMaterialId);
			if (historyDefectiveRavMaterial is null)
			{
				await context
					.WriteResponseAsync(404, $"Записи под id: \"{historyDefectiveRavMaterialId}\" не существует в базе данных")
					.ConfigureAwait(false);
				return;
			}

			var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
			if (!JsonSerializeHelper.TryDeserialize<HistoryDefectiveRavMaterialRequest>(requestBody,
					out var historyDefectiveRavMaterialRequest))
			{
				await context.WriteResponseAsync(400, "Недопустимое содержимое тела запроса").ConfigureAwait(false);
				return;
			}

			var newHistoryDefectiveRavMaterial = historyDefectiveRavMaterialRequest.ToEntity().Result;

			await _companyProvider
				.UpdateHistoryDefectiveRavMaterialAsync(historyDefectiveRavMaterial.Id, newHistoryDefectiveRavMaterial)
				.ConfigureAwait(false);
			await context.WriteResponseAsync(201, null).ConfigureAwait(false);
		}
	}
}