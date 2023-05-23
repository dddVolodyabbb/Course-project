using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Context.Providers.HistoryOfProductSolids;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Requests;
using InventoryServer.Services.JwtToken;

namespace InventoryServer.Commands.HistoryOfProductSold;

public class CreateHistoryOfProductSold : AuthorizationCommand
{
	public override string Path => @"/HistoryOfProductSold/Create";
	public override HttpMethod Method => HttpMethod.Post;
	public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };
	private readonly IHistoryOfProductSoldProvider _historyOfProductSoldProvider;

	public CreateHistoryOfProductSold(IJwtTokenService jwtTokenService, IHistoryOfProductSoldProvider historyOfProductSoldProvider) :
		base(jwtTokenService)
	{
		_historyOfProductSoldProvider = historyOfProductSoldProvider;
	}

	protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
	{
		var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
		if (!JsonSerializeHelper.TryDeserialize<HistoryOfProductSoldRequest>(requestBody,
				out var historyOfProductSoldRequest))
		{
			await context.WriteResponseAsync(400, "Недопустимое содержимое тела запроса").ConfigureAwait(false);
			return;
		}

		var historyOfProductSold = await historyOfProductSoldRequest.ToEntity().ConfigureAwait(false);
		await _historyOfProductSoldProvider.CreateHistoryOfProductSoldAsync(historyOfProductSold)
			.ConfigureAwait(false);
		await context.WriteResponseAsync(201, null).ConfigureAwait(false);
	}
}