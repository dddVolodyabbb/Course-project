using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Context.Providers.DeliveryCompanies;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Services.JwtToken;

namespace InventoryServer.Commands.DeliveryCompany
{
	public class GetAllDeliveryCompany : AuthorizationCommand
	{
		public override string Path => @"/DeliveryCompany/GetAll";
		public override HttpMethod Method => HttpMethod.Get;
		public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin, UserRole.WarehouseUser };
		private readonly IDeliveryCompanyProvider _companyProvider;
		public GetAllDeliveryCompany(IJwtTokenService jwtTokenService, IDeliveryCompanyProvider companyProvider) :
			base(jwtTokenService)
		{
			_companyProvider = companyProvider;
		}

		protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
		{
			var deliveryCompanyCollection = await _companyProvider.GetAllDeliveryCompanyAsync();
			var response = deliveryCompanyCollection.Select(deliveryCompany => deliveryCompany.ToResponse()).ToList();
			await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(response)).ConfigureAwait(false);
		}
	}
}