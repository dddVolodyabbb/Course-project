using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Context.Providers.ProductInOnePackages;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Services.JwtToken;

namespace InventoryServer.Commands.ProductInOnePackage;

public class DeleteProductInOnePackage : AuthorizationCommand
{
	private const string ProductInOnePackageId = "ProductInOnePackageId";

	public override string Path => @$"/ProductInOnePackage/Delete?Id=(?<{ProductInOnePackageId}>\d+)";

	public override HttpMethod Method => HttpMethod.Delete;
	public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };
	private readonly IProductInOnePackageProvider _productInOnePackageProvider;

	public DeleteProductInOnePackage(IJwtTokenService jwtTokenService, IProductInOnePackageProvider productInOnePackageProvider) :
		base(jwtTokenService)
	{
		_productInOnePackageProvider = productInOnePackageProvider;
	}

	protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
	{
		var id = int.Parse(path.Groups[ProductInOnePackageId].Value);

		var productInOnePackage = await _productInOnePackageProvider.GetOneProductInOnePackageAsync(id);
		if (productInOnePackage is null)
		{
			await context.WriteResponseAsync(404, $"Записи под id: \"{id}\" не существует в базе данных")
				.ConfigureAwait(false);
			return;
		}

		await _productInOnePackageProvider.DeleteProductInOnePackageAsync(productInOnePackage.Id)
			.ConfigureAwait(false);
		await context.WriteResponseAsync(201, null).ConfigureAwait(false);
	}
}