using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryServer.Domain.Entities;

namespace InventoryServer.Context.Providers.HistoryDefectiveRavMaterials;

public interface IHistoryDefectiveRavMaterialProvider
{
    Task<ICollection<HistoryDefectiveRavMaterial>> GetAllHistoryDefectiveRavMaterialAsync();
    Task<HistoryDefectiveRavMaterial> GetOneHistoryDefectiveRavMaterialAsync(int historyDefectiveRavMaterialName);
    Task CreateHistoryDefectiveRavMaterialAsync(HistoryDefectiveRavMaterial historyDefectiveRavMaterial);
    Task UpdateHistoryDefectiveRavMaterialAsync(int id, HistoryDefectiveRavMaterial newHistoryDefectiveRavMaterial);
    Task DeleteHistoryDefectiveRavMaterialAsync(int id);
}