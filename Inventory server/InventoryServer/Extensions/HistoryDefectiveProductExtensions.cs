using System.Threading.Tasks;
using InventoryServer.Context.Providers.ProductTypes;
using InventoryServer.Domain.Entities;
using InventoryServer.Requests;
using InventoryServer.Respones;

namespace InventoryServer.Extensions
{
    public static class HistoryDefectiveProductExtensions
    {
        public static HistoryDefectiveProductResponse ToResponse(this HistoryDefectiveProduct historyDefectiveProduct)
        {
            return new HistoryDefectiveProductResponse
            {
                Id = historyDefectiveProduct.Id,
                CostPrice = historyDefectiveProduct.CostPrice,
                DateOfAssignment = historyDefectiveProduct.DateOfAssignment,
                Note = historyDefectiveProduct.Note,
                ProductType = historyDefectiveProduct.ProductType.Name,
                Weight = historyDefectiveProduct.Weight
            };
        }

        public static async Task<HistoryDefectiveProduct> ToEntity(this HistoryDefectiveProductRequest historyDefectiveProductRequest)
        {
            return new HistoryDefectiveProduct
            {
                CostPrice = historyDefectiveProductRequest.CostPrice,
                DateOfAssignment = historyDefectiveProductRequest.DateOfAssignment,
                Note = historyDefectiveProductRequest.Note,
                Weight = historyDefectiveProductRequest.Weight,
                ProductType = await new ProductTypeProvider()
                    .GetProductTypeByNameAsync(historyDefectiveProductRequest.ProductType)
                    .ConfigureAwait(false),
            };
        }
    }
}