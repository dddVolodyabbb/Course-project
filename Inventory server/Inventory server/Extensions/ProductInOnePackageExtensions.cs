
using System.Data.Entity;
using System.Threading.Tasks;
using InventoryServer.Context.Contexts;
using InventoryServer.Context.Requests;
using InventoryServer.Context.Respones;
using InventoryServer.Domain.Entities;

namespace InventoryServer.Context.Extensions
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
            using var db = new ContextInventory();
            return new ProductInOnePackage
            {
                DateOfManufacture = productInOnePackageRequest.DateOfManufacture,
                LaboratoryAnalysis = productInOnePackageRequest.LaboratoryAnalysis,
                Note = productInOnePackageRequest.Note,
                Price = productInOnePackageRequest.Price,
                SellBy = productInOnePackageRequest.SellBy,
                Suitability = productInOnePackageRequest.Suitability,
                Weight = productInOnePackageRequest.Weight,
                ProductType = await db.ProductTypes
                    .FirstAsync(d => d.Name == productInOnePackageRequest.ProductType)
                    .ConfigureAwait(false),
                Warehouse = await db.Warehouses
                    .FirstAsync(d => d.Name == productInOnePackageRequest.Warehouse)
                    .ConfigureAwait(false)
            };
        }
    }
}
