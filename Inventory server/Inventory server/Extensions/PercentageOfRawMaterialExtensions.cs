
using System.Data.Entity;
using System.Threading.Tasks;
using InventoryServer.Context.Contexts;
using InventoryServer.Context.Requests;
using InventoryServer.Context.Respones;
using InventoryServer.Domain.Entities;

namespace InventoryServer.Context.Extensions
{
    public static class PercentageOfRawMaterialExtensions
    {
        public static PercentageOfRawMaterialResponse ToResponse(this PercentageOfRawMaterial percentageOfRawMaterial)
        {
            return new PercentageOfRawMaterialResponse
            {
                Id = percentageOfRawMaterial.Id,
                Percent = percentageOfRawMaterial.Percent,
                ProductType = percentageOfRawMaterial.ProductType.Name,
                RawMaterialType = percentageOfRawMaterial.RawMaterialType.Name
            };
        }

        public static async Task<PercentageOfRawMaterial> ToEntity(this PercentageOfRawMaterialRequest percentageOfRawMaterialRequest)
        {
            using var db = new ContextInventory();
            return new PercentageOfRawMaterial
            {
                Percent = percentageOfRawMaterialRequest.Percent,
                ProductType = await db.ProductTypes
                    .FirstAsync(d => d.Name == percentageOfRawMaterialRequest.ProductType)
                    .ConfigureAwait(false),
                RawMaterialType = await db.RawMaterialsTypes
                    .FirstAsync(d => d.Name == percentageOfRawMaterialRequest.RawMaterialType)
                    .ConfigureAwait(false)
            };
        }
    }
}
