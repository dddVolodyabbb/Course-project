using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using InventoryServer.Context.Contexts;
using InventoryServer.Domain.Entities;

namespace InventoryServer.Context.Providers.ProductTypes
{
	public class ProductTypeProvider : IProductTypeProvider
	{
		public async Task<ICollection<ProductType>> GetAllProductTypeAsync()
		{
			using var dbContextInventoryControl = new ContextInventoryControl();
			return await dbContextInventoryControl.ProductTypes.ToListAsync().ConfigureAwait(false);
		}
		public async Task<ProductType> GetProductTypeByNameAsync(string name)
		{
			using var dbContextInventoryControl = new ContextInventoryControl();
			return await dbContextInventoryControl.ProductTypes
				.FirstAsync(d => d.Name == name)
				.ConfigureAwait(false);
		}

		public async Task<ProductType> GetOneProductTypeAsync(int productTypeId)
		{
			using var dbContextInventoryControl = new ContextInventoryControl();
			return await dbContextInventoryControl.ProductTypes
				.FirstAsync(d => d.Id == productTypeId)
				.ConfigureAwait(false);
		}

		public async Task CreateProductTypeAsync(ProductType productType)
		{
			using var dbContextInventoryControl = new ContextInventoryControl();
			dbContextInventoryControl.ProductTypes.Add(productType);
			await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
		}

		public async Task UpdateProductTypeAsync(int id, ProductType newProductType)
		{
			using var dbContextInventoryControl = new ContextInventoryControl();
			var productType = await dbContextInventoryControl.ProductTypes
				.FirstAsync(d => d.Id == id)
				.ConfigureAwait(false);
			productType.Name = newProductType.Name;
			await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
		}

		public async Task DeleteProductTypeAsync(int id)
		{
			using var dbContextInventoryControl = new ContextInventoryControl();
			var productType = await dbContextInventoryControl.ProductTypes
				.FirstAsync(d => d.Id == id)
				.ConfigureAwait(false);
			dbContextInventoryControl.ProductTypes.Remove(productType);
			await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
		}
	}
}