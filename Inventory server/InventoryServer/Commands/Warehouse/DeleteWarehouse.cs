using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Commands;
using InventoryServer.Context.Providers.Warehouses;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Services.JwtToken;

namespace Inventory_server.Commands.Warehouse
{
	public class DeleteWarehouse : AuthorizationCommand
	{
		private const string WarehouseId = "WarehouseId";
		public override string Path => @$"/Warehouse/Delete?Id=(?<{WarehouseId}>.+)";
		public override HttpMethod Method => HttpMethod.Delete;
		public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };
		private readonly IWarehouseProvider _companyProvider;
		public DeleteWarehouse(IJwtTokenService jwtTokenService, IWarehouseProvider companyProvider) : base(jwtTokenService)
		{
			_companyProvider = companyProvider;
		}

		protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
		{
			var warehouseId = int.Parse(path.Groups[WarehouseId].Value);
			var warehouse = await _companyProvider.GetOneWarehouseAsync(warehouseId);
			if (warehouse is null)
			{
				await context.WriteResponseAsync(404, $"Компании \"{warehouseId}\" не существует в базе данных").ConfigureAwait(false);
				return;
			}

			await _companyProvider.DeleteWarehouseAsync(warehouseId).ConfigureAwait(false);
			await context.WriteResponseAsync(201, null).ConfigureAwait(false);
		}
	}
}