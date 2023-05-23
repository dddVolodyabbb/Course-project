using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Context.Providers.RawMaterialInOnePackages;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Requests;
using InventoryServer.Services.JwtToken;

namespace InventoryServer.Commands.RawMaterialInOnePackage;

public class CreateRawMaterialInOnePackage : AuthorizationCommand
{
	public override string Path => @"/RawMaterialInOnePackage/Create";
	public override HttpMethod Method => HttpMethod.Post;
	public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };
	private readonly IRawMaterialInOnePackageProvider _rawMaterialInOnePackageProvider;

	public CreateRawMaterialInOnePackage(IJwtTokenService jwtTokenService, IRawMaterialInOnePackageProvider rawMaterialInOnePackageProvider) :
        base(jwtTokenService)
	{
		_rawMaterialInOnePackageProvider = rawMaterialInOnePackageProvider;
	}

	protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
	{
		var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
		if (!JsonSerializeHelper.TryDeserialize<RawMaterialInOnePackageRequest>(requestBody,
				out var rawMaterialInOnePackagesRequest))
		{
			await context.WriteResponseAsync(400, "Недопустимое содержимое тела запроса").ConfigureAwait(false);
			return;
		}

		var rawMaterialInOnePackages = await rawMaterialInOnePackagesRequest.ToEntity().ConfigureAwait(false);
		await _rawMaterialInOnePackageProvider.CreateRawMaterialInOnePackageAsync(rawMaterialInOnePackages)
			.ConfigureAwait(false);
		await context.WriteResponseAsync(201, null).ConfigureAwait(false);
	}
}