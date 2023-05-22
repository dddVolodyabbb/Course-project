using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Context.Providers.PercentageOfRawMaterials;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Services.JwtToken;

namespace InventoryServer.Commands.PercentageOfRawMaterial;

public class DeletePercentageOfRawMaterial : AuthorizationCommand
{
	private const string PercentageOfRawMaterialId = "PercentageOfRawMaterialId";

	public override string Path => @$"/PercentageOfRawMaterial/Delete?Id=(?<{PercentageOfRawMaterialId}>\d+)";

	public override HttpMethod Method => HttpMethod.Delete;
	public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };
	private readonly IPercentageOfRawMaterialProvider _percentageOfRawMaterialProvider;

	public DeletePercentageOfRawMaterial(IJwtTokenService jwtTokenService, IPercentageOfRawMaterialProvider percentageOfRawMaterialProvider) :
		base(jwtTokenService)
	{
		_percentageOfRawMaterialProvider = percentageOfRawMaterialProvider;
	}

	protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
	{
		var id = int.Parse(path.Groups[PercentageOfRawMaterialId].Value);

		var percentageOfRawMaterial = await _percentageOfRawMaterialProvider.GetOnePercentageOfRawMaterialAsync(id);
		if (percentageOfRawMaterial is null)
		{
			await context.WriteResponseAsync(404, $"Записи под id: \"{id}\" не существует в базе данных")
				.ConfigureAwait(false);
			return;
		}

		await _percentageOfRawMaterialProvider.DeletePercentageOfRawMaterialAsync(percentageOfRawMaterial.Id)
			.ConfigureAwait(false);
		await context.WriteResponseAsync(201, null).ConfigureAwait(false);
	}
}