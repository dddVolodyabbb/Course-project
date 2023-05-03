using InventoryServer.Context.Contexts;
using InventoryServer.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryServer.Context.Providers.ProductTypes
{
    public interface IProductTypeProvider
    {
        public ContextInventoryControl DbContextInventoryControl { get; }
        Task<ICollection<ProductType>> GetAllProductTypeAsync();
        Task<ProductType> GetOneProductTypeAsync(string productTypeName);
        Task CreateProductTypeAsync(ProductType productType);
        Task UpdateProductTypeAsync(ProductType productType, ProductType newProductType);
        Task DeleteProductTypeAsync(ProductType productType);
    }
}
