using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryServer.Domain.Entities;

namespace InventoryServer.Context.Providers.ProductInOnePackages
{
    public interface IProductInOnePackageProvider
	{
        Task<ICollection<ProductInOnePackage>> GetAllProductInOnePackageAsync();
        Task<ProductInOnePackage> GetOneProductInOnePackageAsync(int productInOnePackagesName);
        Task CreateProductInOnePackageAsync(ProductInOnePackage productInOnePackages);
        Task UpdateProductInOnePackageAsync(int id, ProductInOnePackage newProductInOnePackages);
        Task DeleteProductInOnePackageAsync(int id);
    }
}