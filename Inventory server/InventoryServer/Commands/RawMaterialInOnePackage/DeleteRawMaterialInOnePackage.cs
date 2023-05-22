using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Context.Providers.RawMaterialInOnePackages;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Services.JwtToken;

namespace InventoryServer.Commands.RawMaterialInOnePackage;

public class DeleteRawMaterialInOnePackage : AuthorizationCommand
{
	private const string RawMaterialInOnePackageId = "RawMaterialInOnePackageId";
	public override string Path => @$"/RawMaterialInOnePackage/Delete?Id=(?<{RawMaterialInOnePackageId}>\d+)";
	public override HttpMethod Method => HttpMethod.Delete;
	public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };
	private readonly IRawMaterialInOnePackageProvider _rawMaterialInOnePackageProvider;

	public DeleteRawMaterialInOnePackage(IJwtTokenService jwtTokenService, IRawMaterialInOnePackageProvider rawMaterialInOnePackageProvider) :
		base(jwtTokenService)
	{
		_rawMaterialInOnePackageProvider = rawMaterialInOnePackageProvider;
	}

	protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
	{
		var id = int.Parse(path.Groups[RawMaterialInOnePackageId].Value);

		var rawMaterialInOnePackage = await _rawMaterialInOnePackageProvider.GetOneRawMaterialInOnePackageAsync(id);
		if (rawMaterialInOnePackage is null)
		{
			await context.WriteResponseAsync(404, $"Записи под id: \"{id}\" не существует в базе данных")
				.ConfigureAwait(false);
			return;
		}

		await _rawMaterialInOnePackageProvider.DeleteRawMaterialInOnePackageAsync(rawMaterialInOnePackage.Id)
			.ConfigureAwait(false);
		await context.WriteResponseAsync(201, null).ConfigureAwait(false);
	}
}