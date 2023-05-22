using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Context.Providers.DeliveryCompanies;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Services.JwtToken;

namespace InventoryServer.Commands.DeliveryCompany
{
	public class DeleteDeliveryCompany : AuthorizationCommand
	{
		private const string DeliveryCompanyId = "DeliveryCompanyId";
		public override string Path => @$"/DeliveryCompany/Delete?Id=(?<{DeliveryCompanyId}>.+)";
		public override HttpMethod Method => HttpMethod.Delete;
		public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };
		private readonly IDeliveryCompanyProvider _companyProvider;
		public DeleteDeliveryCompany(IJwtTokenService jwtTokenService, IDeliveryCompanyProvider companyProvider) :
			base(jwtTokenService)
		{
			_companyProvider = companyProvider;
		}

		protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
		{
			var deliveryCompanyId = int.Parse(path.Groups[DeliveryCompanyId].Value);
			var deliveryCompany = await _companyProvider.GetOneDeliveryCompanyAsync(deliveryCompanyId);
			if (deliveryCompany is null)
			{
				await context.WriteResponseAsync(404, $"Компании \"{deliveryCompanyId}\" не существует в базе данных").ConfigureAwait(false);
				return;
			}

			await _companyProvider.DeleteDeliveryCompanyAsync(deliveryCompany.Id).ConfigureAwait(false);
			await context.WriteResponseAsync(201, null).ConfigureAwait(false);
		}
	}
}