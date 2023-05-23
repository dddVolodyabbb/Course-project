using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Context.Providers.PercentageOfRawMaterials;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Requests;
using InventoryServer.Services.JwtToken;

namespace InventoryServer.Commands.PercentageOfRawMaterial;

public class CreatePercentageOfRawMaterial : AuthorizationCommand
{
	public override string Path => @"/PercentageOfRawMaterial/Create";
	public override HttpMethod Method => HttpMethod.Post;
	public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };
	private readonly IPercentageOfRawMaterialProvider _percentageOfRawMaterialProvider;

	public CreatePercentageOfRawMaterial(IJwtTokenService jwtTokenService, IPercentageOfRawMaterialProvider percentageOfRawMaterialProvider) :
		base(jwtTokenService)
	{
		_percentageOfRawMaterialProvider = percentageOfRawMaterialProvider;
	}

	protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
	{
		var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
		if (!JsonSerializeHelper.TryDeserialize<PercentageOfRawMaterialRequest>(requestBody,
				out var percentageOfRawMaterialRequest))
		{
			await context.WriteResponseAsync(400, "Недопустимое содержимое тела запроса").ConfigureAwait(false);
			return;
		}

		var percentageOfRawMaterial = await percentageOfRawMaterialRequest.ToEntity().ConfigureAwait(false);
		await _percentageOfRawMaterialProvider.CreatePercentageOfRawMaterialAsync(percentageOfRawMaterial)
			.ConfigureAwait(false);
		await context.WriteResponseAsync(201, null).ConfigureAwait(false);
	}
}