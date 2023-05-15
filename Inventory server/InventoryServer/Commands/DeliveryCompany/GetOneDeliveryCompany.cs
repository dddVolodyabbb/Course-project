using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Commands;
using InventoryServer.Context.Providers.DeliveryCompanies;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Services.JwtToken;

namespace Inventory_server.Commands.DeliveryCompany
{
	public class GetOneDeliveryCompany : AuthorizationCommand
	{
		private const string DeliveryCompanyId = "DeliveryCompanyId";
		public override string Path => @$"/DeliveryCompany/GetOne?Id=(?<{DeliveryCompanyId}>.+)";
		public override HttpMethod Method => HttpMethod.Get;
		public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin, UserRole.WarehouseUser };
		private readonly IDeliveryCompanyProvider _companyProvider;

		public GetOneDeliveryCompany(IJwtTokenService jwtTokenService, IDeliveryCompanyProvider companyProvider) : base(
			jwtTokenService)
		{
			_companyProvider = companyProvider;
		}

		protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
		{
			var deliveryCompanyId = int.Parse(path.Groups[DeliveryCompanyId].Value);
			var response = await _companyProvider.GetOneDeliveryCompanyAsync(deliveryCompanyId);
			await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(response)).ConfigureAwait(false);
		}
	}
}