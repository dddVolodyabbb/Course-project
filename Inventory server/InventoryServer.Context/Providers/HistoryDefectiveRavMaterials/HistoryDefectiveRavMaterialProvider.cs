using InventoryServer.Context.Contexts;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using InventoryServer.Domain.Entities;

namespace InventoryServer.Context.Providers.HistoryDefectiveRavMaterials
{
    public class HistoryDefectiveRavMaterialProvider : IHistoryDefectiveRavMaterialProvider
    {
        public async Task<ICollection<HistoryDefectiveRavMaterial>> GetAllHistoryDefectiveRavMaterialAsync()
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            return await dbContextInventoryControl.HistoryDefectiveRavMaterials.ToListAsync().ConfigureAwait(false);
        }

        public async Task<HistoryDefectiveRavMaterial> GetOneHistoryDefectiveRavMaterialAsync(int historyDefectiveRavMaterialName)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            return await dbContextInventoryControl.HistoryDefectiveRavMaterials
                .FirstAsync(d => d.Id == historyDefectiveRavMaterialName)
                .ConfigureAwait(false);
        }

        public async Task CreateHistoryDefectiveRavMaterialAsync(HistoryDefectiveRavMaterial historyDefectiveRavMaterial)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            dbContextInventoryControl.HistoryDefectiveRavMaterials.Add(historyDefectiveRavMaterial);
            await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateHistoryDefectiveRavMaterialAsync(int id, HistoryDefectiveRavMaterial newHistoryDefectiveRavMaterial)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            var historyDefectiveRavMaterial = await dbContextInventoryControl.HistoryDefectiveRavMaterials
				.FirstAsync(d => d.Id == id)
				.ConfigureAwait(false);
            historyDefectiveRavMaterial.CostPrice = newHistoryDefectiveRavMaterial.CostPrice;
            historyDefectiveRavMaterial.DateOfAssignment = newHistoryDefectiveRavMaterial.DateOfAssignment;
            historyDefectiveRavMaterial.Note = newHistoryDefectiveRavMaterial.Note;
			historyDefectiveRavMaterial.RawMaterialTypeId = newHistoryDefectiveRavMaterial.RawMaterialTypeId;
            historyDefectiveRavMaterial.Weight = newHistoryDefectiveRavMaterial.Weight;
            await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteHistoryDefectiveRavMaterialAsync(int id)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
			var historyDefectiveRavMaterial = await dbContextInventoryControl.HistoryDefectiveRavMaterials
				.FirstAsync(d => d.Id == id)
				.ConfigureAwait(false);
            dbContextInventoryControl.HistoryDefectiveRavMaterials.Remove(historyDefectiveRavMaterial);
            await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}