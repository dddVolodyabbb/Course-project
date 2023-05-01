
using System.Data.Entity;
using System.Threading.Tasks;
using InventoryServer.Context.Contexts;
using InventoryServer.Context.Requests;
using InventoryServer.Context.Respones;
using InventoryServer.Domain.Entities;

namespace InventoryServer.Context.Extensions
{
    public static class RawMaterialInOnePackageExtensions
    {
        public static RawMaterialInOnePackageResponse ToResponse(this RawMaterialInOnePackage rawMaterialInOnePackage)
        {
            return new RawMaterialInOnePackageResponse
            {
                Id = rawMaterialInOnePackage.Id,
                CostOfDelivery = rawMaterialInOnePackage.CostOfDelivery,
                DateOfManufacture = rawMaterialInOnePackage.DateOfManufacture,
                DeliveryCompany = rawMaterialInOnePackage.DeliveryCompany.Name,
                LaboratoryAnalysis = rawMaterialInOnePackage.LaboratoryAnalysis,
                Note = rawMaterialInOnePackage.Note,
                Price = rawMaterialInOnePackage.Price,
                RawMaterialProducer = rawMaterialInOnePackage.RawMaterialProducer.Name,
                RawMaterialType = rawMaterialInOnePackage.RawMaterialType.Name,
                SellBy = rawMaterialInOnePackage.SellBy,
                Weight = rawMaterialInOnePackage.Weight,
                Suitability = rawMaterialInOnePackage.Suitability,
                Warehouse = rawMaterialInOnePackage.Warehouse.Name
            };
        }

        public static async Task<RawMaterialInOnePackage> ToEntity(this RawMaterialInOnePackageRequest rawMaterialInOnePackageResponse)
        {
            using var db = new ContextInventory();
            return new RawMaterialInOnePackage
            {
                CostOfDelivery = rawMaterialInOnePackageResponse.CostOfDelivery,
                DateOfManufacture = rawMaterialInOnePackageResponse.DateOfManufacture,
                LaboratoryAnalysis = rawMaterialInOnePackageResponse.LaboratoryAnalysis,
                Note = rawMaterialInOnePackageResponse.Note,
                Price = rawMaterialInOnePackageResponse.Price,
                SellBy = rawMaterialInOnePackageResponse.SellBy,
                Weight = rawMaterialInOnePackageResponse.Weight,
                Suitability = rawMaterialInOnePackageResponse.Suitability,
                Warehouse = await db.Warehouses
                    .FirstAsync(d => d.Name == rawMaterialInOnePackageResponse.Warehouse)
                    .ConfigureAwait(false),
                DeliveryCompany = await db.DeliveryCompanies
                    .FirstAsync(d => d.Name == rawMaterialInOnePackageResponse.DeliveryCompany)
                    .ConfigureAwait(false),
                RawMaterialProducer = await db.RawMaterialsProducers
                    .FirstAsync(d => d.Name == rawMaterialInOnePackageResponse.RawMaterialProducer)
                    .ConfigureAwait(false),
                RawMaterialType = await db.RawMaterialsTypes
                    .FirstAsync(d => d.Name == rawMaterialInOnePackageResponse.RawMaterialType)
                    .ConfigureAwait(false),
            };
        }
    }
}
