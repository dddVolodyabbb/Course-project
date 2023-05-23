using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Context.Providers.HistoryDefectiveProducts;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Services.JwtToken;

namespace InventoryServer.Commands.HistoryDefectiveProduct
{
	public class DeleteHistoryDefectiveProduct : AuthorizationCommand
	{
		private const string HistoryDefectiveProductId = "HistoryDefectiveProductId";
		public override string Path => @$"/HistoryDefectiveProduct/Delete?Id=(?<{HistoryDefectiveProductId}>\d+)";
		public override HttpMethod Method => HttpMethod.Delete;
		public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };
		private readonly IHistoryDefectiveProductProvider _historyDefectiveProductProvider;
		public DeleteHistoryDefectiveProduct(IJwtTokenService jwtTokenService, IHistoryDefectiveProductProvider historyDefectiveProductProvider) :
			base(jwtTokenService)
		{
			_historyDefectiveProductProvider = historyDefectiveProductProvider;
		}

		protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
		{
			var id = int.Parse(path.Groups[HistoryDefectiveProductId].Value);
			var historyDefectiveProduct = await _historyDefectiveProductProvider.GetOneHistoryDefectiveProductAsync(id);
			if (historyDefectiveProduct is null)
			{
				await context.WriteResponseAsync(404, $"Записи под id: \"{id}\" не существует в базе данных").ConfigureAwait(false);
				return;
			}

			await _historyDefectiveProductProvider.DeleteHistoryDefectiveProductAsync(historyDefectiveProduct.Id).ConfigureAwait(false);
			await context.WriteResponseAsync(201, null).ConfigureAwait(false);
		}
	}
}