using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InventoryServer.Context.Providers.ProductInOnePackages;
using InventoryServer.Domain.Entities;
using InventoryServer.Extensions;
using InventoryServer.Helpers;
using InventoryServer.Requests;
using InventoryServer.Services.JwtToken;

namespace InventoryServer.Commands.ProductInOnePackage;

public class CreateProductInOnePackage : AuthorizationCommand
{
	public override string Path => @"/ProductInOnePackage/Create";
	public override HttpMethod Method => HttpMethod.Post;
	public override UserRole[] AllowedUserRoles => new[] { UserRole.Admin };
	private readonly IProductInOnePackageProvider _productInOnePackageProvider;

	public CreateProductInOnePackage(IJwtTokenService jwtTokenService, IProductInOnePackageProvider productInOnePackageProvider) :
        base(jwtTokenService)
	{
		_productInOnePackageProvider = productInOnePackageProvider;
	}

	protected override async Task HandleRequestInternalAsync(HttpListenerContext context, Match path)
	{
		var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
		if (!JsonSerializeHelper.TryDeserialize<ProductInOnePackageRequest>(requestBody,
				out var productInOnePackageRequest))
		{
			await context.WriteResponseAsync(400, "Недопустимое содержимое тела запроса").ConfigureAwait(false);
			return;
		}

		var productInOnePackage = await productInOnePackageRequest.ToEntity().ConfigureAwait(false);
		await _productInOnePackageProvider.CreateProductInOnePackageAsync(productInOnePackage)
			.ConfigureAwait(false);
		await context.WriteResponseAsync(201, null).ConfigureAwait(false);
	}
}