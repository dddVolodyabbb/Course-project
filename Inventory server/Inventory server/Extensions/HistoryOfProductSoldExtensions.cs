
using System.Data.Entity;
using System.Threading.Tasks;
using InventoryServer.Context.Contexts;
using InventoryServer.Context.Requests;
using InventoryServer.Context.Respones;
using InventoryServer.Domain.Entities;

namespace InventoryServer.Context.Extensions
{
    public static class HistoryOfProductSoldExtensions
    {
        public static HistoryOfProductSoldResponse ToResponse(this HistoryOfProductSold historyOfProductSold)
        {
            return new HistoryOfProductSoldResponse
            {
                Id = historyOfProductSold.Id,
                DateOfManufacture = historyOfProductSold.DateOfManufacture,
                Price = historyOfProductSold.Price,
                ProductType = historyOfProductSold.ProductType.Name,
                SellBy = historyOfProductSold.SellBy,
                Suitability = historyOfProductSold.Suitability,
                Weight = historyOfProductSold.Weight
                
            };
        }

        public static async Task<HistoryOfProductSold> ToEntity(this HistoryOfProductSoldRequest historyOfProductSoldRequest)
        {
            using var db = new ContextInventory();
            return new HistoryOfProductSold
            {
                DateOfManufacture = historyOfProductSoldRequest.DateOfManufacture,
                Price = historyOfProductSoldRequest.Price,
                ProductType = await db.ProductTypes
                    .FirstAsync(d => d.Name == historyOfProductSoldRequest.ProductType)
                    .ConfigureAwait(false),
                SellBy = historyOfProductSoldRequest.SellBy,
                Suitability = historyOfProductSoldRequest.Suitability,
                Weight = historyOfProductSoldRequest.Weight
                
            };
        }
    }
}
