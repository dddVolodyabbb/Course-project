using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using InventoryServer.Context.Contexts;
using InventoryServer.Domain.Entities;

namespace InventoryServer.Context.Providers.ProductInOnePackages
{
	public class ProductInOnePackageProvider : IProductInOnePackageProvider
	{
		public async Task<ICollection<ProductInOnePackage>> GetAllProductInOnePackageAsync()
		{
			using var dbContextInventoryControl = new ContextInventoryControl();
			return await dbContextInventoryControl.ProductInOnePackages.ToListAsync().ConfigureAwait(false);
		}

		public async Task<ProductInOnePackage> GetOneProductInOnePackageAsync(int productInOnePackageName)
		{
			using var dbContextInventoryControl = new ContextInventoryControl();
			return await dbContextInventoryControl.ProductInOnePackages
				.FirstAsync(d => d.Id == productInOnePackageName)
				.ConfigureAwait(false);
		}

		public async Task CreateProductInOnePackageAsync(ProductInOnePackage productInOnePackage)
		{
			using var dbContextInventoryControl = new ContextInventoryControl();
			dbContextInventoryControl.ProductInOnePackages.Add(productInOnePackage);
			await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
		}

		public async Task UpdateProductInOnePackageAsync(int id, ProductInOnePackage newProductInOnePackage)
		{
			using var dbContextInventoryControl = new ContextInventoryControl();
			var productInOnePackage = await dbContextInventoryControl.ProductInOnePackages
				.FirstAsync(d => d.Id == id)
				.ConfigureAwait(false);
            productInOnePackage.ProductTypeId = newProductInOnePackage.ProductTypeId;
            productInOnePackage.DateOfManufacture = newProductInOnePackage.DateOfManufacture;
            productInOnePackage.LaboratoryAnalysis = newProductInOnePackage.LaboratoryAnalysis;
            productInOnePackage.Note = newProductInOnePackage.Note;
            productInOnePackage.Price = newProductInOnePackage.Price;
            productInOnePackage.SellBy = newProductInOnePackage.SellBy;
            productInOnePackage.Warehouse = newProductInOnePackage.Warehouse;
            productInOnePackage.Suitability = newProductInOnePackage.Suitability;
            await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
		}

		public async Task DeleteProductInOnePackageAsync(int id)
		{
			using var dbContextInventoryControl = new ContextInventoryControl();
			var productInOnePackage = await dbContextInventoryControl.ProductInOnePackages
				.FirstAsync(d => d.Id == id)
				.ConfigureAwait(false);
            dbContextInventoryControl.ProductInOnePackages.Remove(productInOnePackage);
			await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
		}
	}
}