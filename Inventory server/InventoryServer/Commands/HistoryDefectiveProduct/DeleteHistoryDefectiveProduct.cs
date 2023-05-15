using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Commands;
using InventoryServer.Context.Providers.HistoryDefectiveProducts;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Services.JwtToken;

namespace Inventory_server.Commands.HistoryDefectiveProduct
{
	public class DeleteHistoryDefectiveProduct : AuthorizationCommand
	{
		private const string HistoryDefectiveProductId = "HistoryDefectiveProductId";
		public override string Path => @$"/HistoryDefectiveProduct/Delete?Id=(?<{HistoryDefectiveProductId}>\d+)";
		public override HttpMethod Method => HttpMethod.Delete;
		public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };
		private readonly IHistoryDefectiveProductProvider _companyProvider;
		public DeleteHistoryDefectiveProduct(IJwtTokenService jwtTokenService, IHistoryDefectiveProductProvider companyProvider) : base(jwtTokenService)
		{
			_companyProvider = companyProvider;
		}

		protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
		{
			var id = int.Parse(path.Groups[HistoryDefectiveProductId].Value);

			var historyDefectiveProduct = await _companyProvider.GetOneHistoryDefectiveProductAsync(id);
			if (historyDefectiveProduct is null)
			{
				await context.WriteResponseAsync(404, $"Записи под id: \"{id}\" не существует в базе данных").ConfigureAwait(false);
				return;
			}

			await _companyProvider.DeleteHistoryDefectiveProductAsync(historyDefectiveProduct.Id).ConfigureAwait(false);
			await context.WriteResponseAsync(201, null).ConfigureAwait(false);
		}
	}
}