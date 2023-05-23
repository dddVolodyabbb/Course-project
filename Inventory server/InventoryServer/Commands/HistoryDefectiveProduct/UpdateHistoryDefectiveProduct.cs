using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Context.Providers.HistoryDefectiveProducts;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Requests;
using InventoryServer.Services.JwtToken;

namespace InventoryServer.Commands.HistoryDefectiveProduct
{
	public class UpdateHistoryDefectiveProduct : AuthorizationCommand
	{
		private const string HistoryDefectiveProductId = "HistoryDefectiveProductId";
		public override string Path => @$"/HistoryDefectiveProduct/Update?Id=(?<{HistoryDefectiveProductId}>.+)";
		public override HttpMethod Method => HttpMethod.Put;
		public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };
		private readonly IHistoryDefectiveProductProvider _historyDefectiveProductProvider;

		public UpdateHistoryDefectiveProduct(IJwtTokenService jwtTokenService, IHistoryDefectiveProductProvider historyDefectiveProductProvider) :
			base(jwtTokenService)
		{
			_historyDefectiveProductProvider = historyDefectiveProductProvider;
		}

		protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
		{
			var historyDefectiveProductId = int.Parse(path.Groups[HistoryDefectiveProductId].Value);
			var historyDefectiveProduct =
				await _historyDefectiveProductProvider.GetOneHistoryDefectiveProductAsync(historyDefectiveProductId);
			if (historyDefectiveProduct is null)
			{
				await context
					.WriteResponseAsync(404, $"Записи под id: \"{historyDefectiveProductId}\" не существует в базе данных")
					.ConfigureAwait(false);
				return;
			}

			var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
			if (!JsonSerializeHelper.TryDeserialize<HistoryDefectiveProductRequest>(requestBody,
					out var historyDefectiveProductRequest))
			{
				await context.WriteResponseAsync(400, "Недопустимое содержимое тела запроса").ConfigureAwait(false);
				return;
			}

			var newHistoryDefectiveProduct = historyDefectiveProductRequest.ToEntity().Result;
			await _historyDefectiveProductProvider
				.UpdateHistoryDefectiveProductAsync(historyDefectiveProduct.Id, newHistoryDefectiveProduct)
				.ConfigureAwait(false);
			await context.WriteResponseAsync(201, null).ConfigureAwait(false);
		}
	}
}