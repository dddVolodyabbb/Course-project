using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Context.Providers.HistoryOfProductSolids;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Services.JwtToken;

namespace InventoryServer.Commands.HistoryOfProductSold
{
	public class GetAllHistoryOfProductSold : AuthorizationCommand
	{
		public override string Path => @"/HistoryOfProductSold/GetAll";
		public override HttpMethod Method => HttpMethod.Get;
		public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin, UserRole.WarehouseUser };
		private readonly IHistoryOfProductSoldProvider _companyProvider;

		public GetAllHistoryOfProductSold(IJwtTokenService jwtTokenService, IHistoryOfProductSoldProvider companyProvider) :
			base(jwtTokenService)
		{
			_companyProvider = companyProvider;
		}

		protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
		{
			var historyOfProductSoldCollection = await _companyProvider.GetAllHistoryOfProductSoldAsync();
			var response = historyOfProductSoldCollection
				.Select(historyOfProductSold => historyOfProductSold.ToResponse()).ToList();
			await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(response)).ConfigureAwait(false);
		}
	}
}