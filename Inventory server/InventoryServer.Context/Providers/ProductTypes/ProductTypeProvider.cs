
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

        public async Task<ProductType> GetOneProductTypeAsync(string productTypeName)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            return await dbContextInventoryControl.ProductTypes
                .FirstAsync(d => d.Name == productTypeName)
                .ConfigureAwait(false);
        }

        public async Task CreateProductTypeAsync(ProductType productType)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            dbContextInventoryControl.ProductTypes.Add(productType);
            await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateProductTypeAsync(ProductType productTypeName, ProductType newProductType)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            productTypeName.Name = newProductType.Name;
            await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteProductTypeAsync(ProductType productType)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            dbContextInventoryControl.ProductTypes.Remove(productType);
            await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
