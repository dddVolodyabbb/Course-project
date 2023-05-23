using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryServer.Domain.Entities;

namespace InventoryServer.Context.Providers.HistoryOfProductSolids;

public interface IHistoryOfProductSoldProvider
{
	Task<ICollection<HistoryOfProductSold>> GetAllHistoryOfProductSoldAsync();
	Task<HistoryOfProductSold> GetOneHistoryOfProductSoldAsync(int historyOfProductSoldName);
	Task CreateHistoryOfProductSoldAsync(HistoryOfProductSold historyOfProductSold);
	Task UpdateHistoryOfProductSoldAsync(int id, HistoryOfProductSold newHistoryOfProductSold);
	Task DeleteHistoryOfProductSoldAsync(int id);
}