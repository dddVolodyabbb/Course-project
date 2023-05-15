using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryServer.Domain.Entities;

namespace InventoryServer.Context.Providers;

public interface IRawMaterialTypeProvider
{
    Task<ICollection<RawMaterialType>> GetAllRawMaterialTypeAsync();
	Task<RawMaterialType> GetRawMaterialTypeByNameAsync(string name);
    Task<RawMaterialType> GetOneRawMaterialTypeAsync(int rawMaterialTypeId);
    Task CreateRawMaterialTypeAsync(RawMaterialType rawMaterialType);
    Task UpdateRawMaterialTypeAsync(int id, RawMaterialType newRawMaterialType);
    Task DeleteRawMaterialTypeAsync(int id);
}