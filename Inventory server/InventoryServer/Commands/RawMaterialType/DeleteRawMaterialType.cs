using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Commands;
using InventoryServer.Context.Providers;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Services.JwtToken;

namespace Inventory_server.Commands.RawMaterialType
{
	public class DeleteRawMaterialType : AuthorizationCommand
	{
		private const string RawMaterialTypeId = "RawMaterialTypeId";
		public override string Path => @$"/RawMaterialType/Delete?Id=(?<{RawMaterialTypeId}>.+)";
		public override HttpMethod Method => HttpMethod.Delete;
		public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };
		private readonly IRawMaterialTypeProvider _companyProvider;
		public DeleteRawMaterialType(IJwtTokenService jwtTokenService, IRawMaterialTypeProvider companyProvider) : base(jwtTokenService)
		{
			_companyProvider = companyProvider;
		}

		protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
		{
			var rawMaterialTypeId = int.Parse(path.Groups[RawMaterialTypeId].Value);
			var rawMaterialType = await _companyProvider.GetOneRawMaterialTypeAsync(rawMaterialTypeId);
			if (rawMaterialType is null)
			{
				await context.WriteResponseAsync(404, $"Компании \"{rawMaterialTypeId}\" не существует в базе данных").ConfigureAwait(false);
				return;
			}

			await _companyProvider.DeleteRawMaterialTypeAsync(rawMaterialTypeId).ConfigureAwait(false);
			await context.WriteResponseAsync(201, null).ConfigureAwait(false);
		}
	}
}