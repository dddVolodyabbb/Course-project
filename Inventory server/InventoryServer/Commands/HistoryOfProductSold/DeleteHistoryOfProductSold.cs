using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Context.Providers.HistoryOfProductSolids;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Services.JwtToken;

namespace InventoryServer.Commands.HistoryOfProductSold;

public class DeleteHistoryOfProductSold : AuthorizationCommand
{
	private const string HistoryOfProductSoldId = "HistoryOfProductSoldId";

	public override string Path => @$"/HistoryOfProductSold/Delete?Id=(?<{HistoryOfProductSoldId}>\d+)";

	public override HttpMethod Method => HttpMethod.Delete;
	public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };
	private readonly IHistoryOfProductSoldProvider _historyOfProductSoldProvider;

	public DeleteHistoryOfProductSold(IJwtTokenService jwtTokenService, IHistoryOfProductSoldProvider historyOfProductSoldProvider) :
		base(jwtTokenService)
	{
		_historyOfProductSoldProvider = historyOfProductSoldProvider;
	}

	protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
	{
		var id = int.Parse(path.Groups[HistoryOfProductSoldId].Value);

		var historyOfProductSold = await _historyOfProductSoldProvider.GetOneHistoryOfProductSoldAsync(id);
		if (historyOfProductSold is null)
		{
			await context.WriteResponseAsync(404, $"Записи под id: \"{id}\" не существует в базе данных")
				.ConfigureAwait(false);
			return;
		}

		await _historyOfProductSoldProvider.DeleteHistoryOfProductSoldAsync(historyOfProductSold.Id)
			.ConfigureAwait(false);
		await context.WriteResponseAsync(201, null).ConfigureAwait(false);
	}
}