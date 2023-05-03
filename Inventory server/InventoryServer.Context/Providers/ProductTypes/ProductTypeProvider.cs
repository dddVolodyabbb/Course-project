
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using InventoryServer.Context.Contexts;
using InventoryServer.Domain.Entities;

namespace InventoryServer.Context.Providers.ProductTypes
{
    public class ProductTypeProvider : IProductTypeProvider
    {
        public ContextInventoryControl DbContextInventoryControl => new();
        public async Task<ICollection<ProductType>> GetAllProductTypeAsync()
        {
            return await DbContextInventoryControl.ProductTypes.ToListAsync().ConfigureAwait(false);
        }

        public async Task<ProductType> GetOneProductTypeAsync(string productTypeName)
        {
            return await DbContextInventoryControl.ProductTypes
                .FirstAsync(d => d.Name == productTypeName)
                .ConfigureAwait(false);
        }

        public async Task CreateProductTypeAsync(ProductType productType)
        {
            DbContextInventoryControl.ProductTypes.Add(productType);
            await DbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateProductTypeAsync(ProductType productTypeName, ProductType newProductType)
        {
            productTypeName.Name = newProductType.Name;
            await DbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteProductTypeAsync(ProductType productType)
        {
            DbContextInventoryControl.ProductTypes.Remove(productType);
            await DbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
