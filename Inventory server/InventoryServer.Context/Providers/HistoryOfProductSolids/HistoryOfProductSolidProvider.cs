using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using InventoryServer.Context.Contexts;
using InventoryServer.Domain.Entities;

namespace InventoryServer.Context.Providers.HistoryOfProductSolids
{
    public class HistoryOfProductSoldProvider : IHistoryOfProductSoldProvider
    {
        public async Task<ICollection<HistoryOfProductSold>> GetAllHistoryOfProductSoldAsync()
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            return await dbContextInventoryControl.HistoryOfProductsSolids.ToListAsync().ConfigureAwait(false);
        }

        public async Task<HistoryOfProductSold> GetOneHistoryOfProductSoldAsync(int HistoryOfProductSoldName)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            return await dbContextInventoryControl.HistoryOfProductsSolids
                .FirstAsync(d => d.Id == HistoryOfProductSoldName)
                .ConfigureAwait(false);
        }

        public async Task CreateHistoryOfProductSoldAsync(HistoryOfProductSold historyOfProductSold)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            dbContextInventoryControl.HistoryOfProductsSolids.Add(historyOfProductSold);
            await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateHistoryOfProductSoldAsync(int id, HistoryOfProductSold newHistoryOfProductSold)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            var historyOfProductSold = await dbContextInventoryControl.HistoryOfProductsSolids
                .FirstAsync(d => d.Id == id)
				.ConfigureAwait(false);
            historyOfProductSold.Price = newHistoryOfProductSold.Price;
            historyOfProductSold.DateOfManufacture = newHistoryOfProductSold.DateOfManufacture;
            historyOfProductSold.ProductTypeId = newHistoryOfProductSold.ProductTypeId;
			historyOfProductSold.SellBy = newHistoryOfProductSold.SellBy;
            historyOfProductSold.Weight = newHistoryOfProductSold.Weight;
            historyOfProductSold.Suitability = newHistoryOfProductSold.Suitability;
            await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteHistoryOfProductSoldAsync(int id)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
			var historyOfProductSold = await dbContextInventoryControl.HistoryOfProductsSolids
                .FirstAsync(d => d.Id == id)
				.ConfigureAwait(false);
            dbContextInventoryControl.HistoryOfProductsSolids.Remove(historyOfProductSold);
            await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}