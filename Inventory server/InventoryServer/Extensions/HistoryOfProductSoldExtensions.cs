using System.Threading.Tasks;
using InventoryServer.Context.Providers.ProductTypes;
using InventoryServer.Domain.Entities;
using InventoryServer.Requests;
using InventoryServer.Respones;

namespace InventoryServer.Extensions
{
	public static class HistoryOfProductSoldExtensions
	{
		public static HistoryOfProductSoldResponse ToResponse(this HistoryOfProductSold historyOfProductSolid)
		{
			return new HistoryOfProductSoldResponse
			{
				Id = historyOfProductSolid.Id,
				DateOfManufacture = historyOfProductSolid.DateOfManufacture,
				Price = historyOfProductSolid.Price,
				ProductType = historyOfProductSolid.ProductType.Name,
				SellBy = historyOfProductSolid.SellBy,
				Suitability = historyOfProductSolid.Suitability,
				Weight = historyOfProductSolid.Weight
			};
		}

		public static async Task<HistoryOfProductSold> ToEntity(this HistoryOfProductSoldRequest historyOfProductSoldRequest)
		{
			return new HistoryOfProductSold
			{
				DateOfManufacture = historyOfProductSoldRequest.DateOfManufacture,
				Price = historyOfProductSoldRequest.Price,
				SellBy = historyOfProductSoldRequest.SellBy,
				Suitability = historyOfProductSoldRequest.Suitability,
				Weight = historyOfProductSoldRequest.Weight,
				ProductType = await new ProductTypeProvider()
					.GetProductTypeByNameAsync(historyOfProductSoldRequest.ProductType)
					.ConfigureAwait(false),
			};
		}
	}
}