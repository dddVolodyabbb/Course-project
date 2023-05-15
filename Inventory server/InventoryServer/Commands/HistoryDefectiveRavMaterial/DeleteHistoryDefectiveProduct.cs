using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Commands;
using InventoryServer.Context.Providers.HistoryDefectiveRavMaterials;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Services.JwtToken;

namespace Inventory_server.Commands.HistoryDefectiveRavMaterial
{
	public class DeleteHistoryDefectiveRavMaterial : AuthorizationCommand
	{
		private const string HistoryDefectiveRavMaterialId = "HistoryDefectiveRavMaterialId";

		public override string Path => @$"/HistoryDefectiveRavMaterial/Delete?Id=(?<{HistoryDefectiveRavMaterialId}>\d+)";

		public override HttpMethod Method => HttpMethod.Delete;
		public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };
		private readonly IHistoryDefectiveRavMaterialProvider _companyProvider;

		public DeleteHistoryDefectiveRavMaterial(IJwtTokenService jwtTokenService, IHistoryDefectiveRavMaterialProvider companyProvider) : 
			base(jwtTokenService)
		{
			_companyProvider = companyProvider;
		}

		protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
		{
			var id = int.Parse(path.Groups[HistoryDefectiveRavMaterialId].Value);

			var historyDefectiveRavMaterial = await _companyProvider.GetOneHistoryDefectiveRavMaterialAsync(id);
			if (historyDefectiveRavMaterial is null)
			{
				await context.WriteResponseAsync(404, $"Записи под id: \"{id}\" не существует в базе данных")
					.ConfigureAwait(false);
				return;
			}

			await _companyProvider.DeleteHistoryDefectiveRavMaterialAsync(historyDefectiveRavMaterial.Id)
				.ConfigureAwait(false);
			await context.WriteResponseAsync(201, null).ConfigureAwait(false);
		}
	}
}