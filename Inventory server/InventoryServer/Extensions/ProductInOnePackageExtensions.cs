
using System.Threading.Tasks;
using InventoryServer.Context.Providers.ProductTypes;
using InventoryServer.Context.Providers.Warehouses;
using InventoryServer.Domain.Entities;
using InventoryServer.Requests;
using InventoryServer.Respones;

namespace InventoryServer.Extensions
{
    public static class ProductInOnePackageExtensions
    {
        public static ProductInOnePackageResponse ToResponse(this ProductInOnePackage productInOnePackage)
        {
            return new ProductInOnePackageResponse
            {
                Id = productInOnePackage.Id,
                DateOfManufacture = productInOnePackage.DateOfManufacture,
                LaboratoryAnalysis = productInOnePackage.LaboratoryAnalysis,
                Note = productInOnePackage.Note,
                Price = productInOnePackage.Price,
                ProductType = productInOnePackage.ProductType.Name,
                SellBy = productInOnePackage.SellBy,
                Suitability = productInOnePackage.Suitability,
                Warehouse = productInOnePackage.Warehouse.Name,
                Weight = productInOnePackage.Weight
            };
        }

        public static async Task<ProductInOnePackage> ToEntity(this ProductInOnePackageRequest productInOnePackageRequest)
        {
            return new ProductInOnePackage
            {
                DateOfManufacture = productInOnePackageRequest.DateOfManufacture,
                LaboratoryAnalysis = productInOnePackageRequest.LaboratoryAnalysis,
                Note = productInOnePackageRequest.Note,
                Price = productInOnePackageRequest.Price,
                SellBy = productInOnePackageRequest.SellBy,
                Suitability = productInOnePackageRequest.Suitability,
                Weight = productInOnePackageRequest.Weight,
                ProductType = await new ProductTypeProvider()
                    .GetProductTypeByNameAsync(productInOnePackageRequest.ProductType)
                    .ConfigureAwait(false),
                Warehouse = await new WarehouseProvider()
                    .GetWarehouseByNameAsync(productInOnePackageRequest.Warehouse)
                    .ConfigureAwait(false)
            };
        }
    }
}
