
using System.Threading.Tasks;
using InventoryServer.Context.Contexts;
using InventoryServer.Context.Providers.ProductTypes;
using InventoryServer.Context.Providers.RawMaterialTypes;
using InventoryServer.Domain.Entities;
using InventoryServer.Requests;
using InventoryServer.Respones;

namespace InventoryServer.Extensions
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
            using var db = new ContextInventoryControl();
            return new PercentageOfRawMaterial
            {
                Percent = percentageOfRawMaterialRequest.Percent,
                ProductType = await new ProductTypeProvider()
                    .GetProductTypeByNameAsync(percentageOfRawMaterialRequest.ProductType)
                    .ConfigureAwait(false),
                RawMaterialType = await new RawMaterialTypeProvider()
                    .GetRawMaterialTypeByNameAsync(percentageOfRawMaterialRequest.RawMaterialType)
                    .ConfigureAwait(false),
            };
        }
    }
}
