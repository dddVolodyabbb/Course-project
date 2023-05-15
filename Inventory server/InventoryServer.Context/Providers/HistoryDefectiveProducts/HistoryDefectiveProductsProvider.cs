using InventoryServer.Context.Contexts;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using InventoryServer.Domain.Entities;

namespace InventoryServer.Context.Providers.HistoryDefectiveProducts
{
	public class HistoryDefectiveProductProvider : IHistoryDefectiveProductProvider
	{
		public async Task<ICollection<HistoryDefectiveProduct>> GetAllHistoryDefectiveProductAsync()
		{
			using var dbContextInventoryControl = new ContextInventoryControl();
			return await dbContextInventoryControl.HistoryDefectiveProducts.ToListAsync().ConfigureAwait(false);
		}

		public async Task<HistoryDefectiveProduct> GetOneHistoryDefectiveProductAsync(int historyDefectiveProductId)
		{
			using var dbContextInventoryControl = new ContextInventoryControl();
			return await dbContextInventoryControl.HistoryDefectiveProducts
				.FirstAsync(d => d.Id == historyDefectiveProductId)
				.ConfigureAwait(false);
		}

		public async Task CreateHistoryDefectiveProductAsync(HistoryDefectiveProduct historyDefectiveProduct)
		{
			using var dbContextInventoryControl = new ContextInventoryControl();
			dbContextInventoryControl.HistoryDefectiveProducts.Add(historyDefectiveProduct);
			await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
		}

		public async Task UpdateHistoryDefectiveProductAsync(int id, HistoryDefectiveProduct newHistoryDefectiveProduct)
		{
			using var dbContextInventoryControl = new ContextInventoryControl();
			var historyDefectiveProduct = await dbContextInventoryControl.HistoryDefectiveProducts
				.FirstAsync(d => d.Id == id)
				.ConfigureAwait(false);
			historyDefectiveProduct.CostPrice = newHistoryDefectiveProduct.CostPrice;
			historyDefectiveProduct.DateOfAssignment = newHistoryDefectiveProduct.DateOfAssignment;
			historyDefectiveProduct.Note = newHistoryDefectiveProduct.Note;
			historyDefectiveProduct.ProductTypeId = newHistoryDefectiveProduct.ProductTypeId;
			historyDefectiveProduct.Weight = newHistoryDefectiveProduct.Weight;
			await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
		}

		public async Task DeleteHistoryDefectiveProductAsync(int id)
		{
			using var dbContextInventoryControl = new ContextInventoryControl();
			var historyDefectiveProduct = await dbContextInventoryControl.HistoryDefectiveProducts
				.FirstAsync(d => d.Id == id)
				.ConfigureAwait(false);
            dbContextInventoryControl.HistoryDefectiveProducts.Remove(historyDefectiveProduct);
			await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
		}
	}
}