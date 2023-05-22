using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryServer.Domain.Entities;

namespace InventoryServer.Context.Providers.PercentageOfRawMaterials
{
    public interface IPercentageOfRawMaterialProvider
	{
        Task<ICollection<PercentageOfRawMaterial>> GetAllPercentageOfRawMaterialAsync();
        Task<PercentageOfRawMaterial> GetOnePercentageOfRawMaterialAsync(int percentageOfRawMaterialName);
        Task CreatePercentageOfRawMaterialAsync(PercentageOfRawMaterial percentageOfRawMaterial);
        Task UpdatePercentageOfRawMaterialAsync(int id, PercentageOfRawMaterial newPercentageOfRawMaterial);
        Task DeletePercentageOfRawMaterialAsync(int id);
    }
}