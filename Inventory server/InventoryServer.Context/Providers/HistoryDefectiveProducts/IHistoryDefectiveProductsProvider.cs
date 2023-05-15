using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryServer.Domain.Entities;

namespace InventoryServer.Context.Providers.HistoryDefectiveProducts;

public interface IHistoryDefectiveProductProvider
{
    Task<ICollection<HistoryDefectiveProduct>> GetAllHistoryDefectiveProductAsync();
    Task<HistoryDefectiveProduct> GetOneHistoryDefectiveProductAsync(int historyDefectiveProductId);
    Task CreateHistoryDefectiveProductAsync(HistoryDefectiveProduct historyDefectiveProduct);
    Task UpdateHistoryDefectiveProductAsync(int id, HistoryDefectiveProduct newHistoryDefectiveProduct);
    Task DeleteHistoryDefectiveProductAsync(int id);
}