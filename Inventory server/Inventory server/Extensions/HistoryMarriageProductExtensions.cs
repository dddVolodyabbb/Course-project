
using System.Data.Entity;
using InventoryServer.Domain.Entities;
using System.Threading.Tasks;
using InventoryServer.Context.Contexts;
using InventoryServer.Context.Requests;
using InventoryServer.Context.Respones;

namespace InventoryServer.Context.Extensions
{
    public static class HistoryMarriageProductExtensions
    {
        public static HistoryMarriageProductResponse ToResponse(this HistoryMarriageProduct historyMarriageProduct)
        {
            return new HistoryMarriageProductResponse
            {
                Id = historyMarriageProduct.Id,
                CostPrice = historyMarriageProduct.CostPrice,
                DateOfAssignment = historyMarriageProduct.DateOfAssignment,
                Note = historyMarriageProduct.Note,
                ProductType = historyMarriageProduct.ProductType.Name,
                Weight = historyMarriageProduct.Weight
            };
        }

        public static async Task<HistoryMarriageProduct> ToEntity(this HistoryMarriageProductRequest historyMarriageProductRequest)
        {
            using var db = new ContextInventory();
            return new HistoryMarriageProduct
            {
                CostPrice = historyMarriageProductRequest.CostPrice,
                DateOfAssignment = historyMarriageProductRequest.DateOfAssignment,
                Note = historyMarriageProductRequest.Note,
                Weight = historyMarriageProductRequest.Weight,
                ProductType = await db.ProductTypes
                    .FirstAsync(d => d.Name == historyMarriageProductRequest.ProductType)
                    .ConfigureAwait(false),
            };
        }
    }

    
}
