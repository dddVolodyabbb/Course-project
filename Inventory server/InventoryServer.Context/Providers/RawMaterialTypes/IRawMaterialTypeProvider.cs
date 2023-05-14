using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryServer.Domain.Entities;

namespace InventoryServer.Context.Providers;

public interface IRawMaterialTypeProvider
{
    Task<ICollection<RawMaterialType>> GetAllRawMaterialTypeAsync();
    Task<RawMaterialType> GetOneRawMaterialTypeAsync(string rawMaterialTypeName);
    Task CreateRawMaterialTypeAsync(RawMaterialType rawMaterialType);
    Task UpdateRawMaterialTypeAsync(RawMaterialType rawMaterialType, RawMaterialType newRawMaterialType);
    Task DeleteRawMaterialTypeAsync(RawMaterialType rawMaterialType);
}