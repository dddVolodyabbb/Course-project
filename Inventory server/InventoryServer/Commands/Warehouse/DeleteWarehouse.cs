using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Context.Providers.Warehouses;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Services.JwtToken;

namespace InventoryServer.Commands.Warehouse
{
	public class DeleteWarehouse : AuthorizationCommand
	{
		private const string WarehouseId = "WarehouseId";
		public override string Path => @$"/Warehouse/Delete?Id=(?<{WarehouseId}>.+)";
		public override HttpMethod Method => HttpMethod.Delete;
		public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };
		private readonly IWarehouseProvider _warehouseProvider;
		public DeleteWarehouse(IJwtTokenService jwtTokenService, IWarehouseProvider warehouseProvider) :
			base(jwtTokenService)
		{
			_warehouseProvider = warehouseProvider;
		}

		protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
		{
			var warehouseId = int.Parse(path.Groups[WarehouseId].Value);
			var warehouse = await _warehouseProvider.GetOneWarehouseAsync(warehouseId);
			if (warehouse is null)
			{
				await context.WriteResponseAsync(404, $"Компании \"{warehouseId}\" не существует в базе данных").ConfigureAwait(false);
				return;
			}

			await _warehouseProvider.DeleteWarehouseAsync(warehouseId).ConfigureAwait(false);
			await context.WriteResponseAsync(201, null).ConfigureAwait(false);
		}
	}
}