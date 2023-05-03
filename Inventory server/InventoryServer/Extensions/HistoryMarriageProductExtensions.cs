
using System.Threading.Tasks;
using InventoryServer.Context.Providers.ProductTypes;
using InventoryServer.Domain.Entities;
using InventoryServer.Requests;
using InventoryServer.Respones;

namespace InventoryServer.Extensions
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
            return new HistoryMarriageProduct
            {
                CostPrice = historyMarriageProductRequest.CostPrice,
                DateOfAssignment = historyMarriageProductRequest.DateOfAssignment,
                Note = historyMarriageProductRequest.Note,
                Weight = historyMarriageProductRequest.Weight,
                ProductType = await new ProductTypeProvider()
                    .GetOneProductTypeAsync(historyMarriageProductRequest.ProductType)
                    .ConfigureAwait(false),
            };
        }
    }

    
}
