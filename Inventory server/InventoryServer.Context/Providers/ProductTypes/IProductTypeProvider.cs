using InventoryServer.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryServer.Context.Providers.ProductTypes
{
	public interface IProductTypeProvider
	{
		Task<ICollection<ProductType>> GetAllProductTypeAsync();
		Task<ProductType> GetProductTypeByNameAsync(string name);
        Task<ProductType> GetOneProductTypeAsync(int productTypeId);
		Task CreateProductTypeAsync(ProductType productType);
		Task UpdateProductTypeAsync(int id, ProductType newProductType);
		Task DeleteProductTypeAsync(int id);
	}
}