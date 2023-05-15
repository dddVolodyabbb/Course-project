using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Commands;
using InventoryServer.Context.Providers.RawMaterialProducers;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Services.JwtToken;

namespace Inventory_server.Commands.RawMaterialProducer
{
	public class DeleteRawMaterialProducer : AuthorizationCommand
	{
		private const string RawMaterialProducerId = "RawMaterialProducerId";
		public override string Path => @$"/RawMaterialProducer/Delete?Id=(?<{RawMaterialProducerId}>.+)";
		public override HttpMethod Method => HttpMethod.Delete;
		public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };
		private readonly IRawMaterialProducerProvider _companyProvider;
		public DeleteRawMaterialProducer(IJwtTokenService jwtTokenService, IRawMaterialProducerProvider companyProvider) :
			base(jwtTokenService)
		{
			_companyProvider = companyProvider;
		}

		protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
		{
			var rawMaterialProducerId = int.Parse(path.Groups[RawMaterialProducerId].Value);
			var rawMaterialProducer = await _companyProvider.GetOneRawMaterialProducerAsync(rawMaterialProducerId);
			if (rawMaterialProducer is null)
			{
				await context.WriteResponseAsync(404, $"Компании \"{rawMaterialProducerId}\" не существует в базе данных").ConfigureAwait(false);
				return;
			}

			await _companyProvider.DeleteRawMaterialProducerAsync(rawMaterialProducerId).ConfigureAwait(false);
			await context.WriteResponseAsync(201, null).ConfigureAwait(false);
		}
	}
}