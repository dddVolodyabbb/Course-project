using InventoryServer.Context.Contexts;
using InventoryServer.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryServer.Context.Providers
{
    public interface IRawMaterialTypeProvider
    {
        public ContextInventoryControl DbContextInventoryControl { get; }
        Task<ICollection<RawMaterialType>> GetAllRawMaterialTypeAsync();
        Task<RawMaterialType> GetOneRawMaterialTypeAsync(string rawMaterialTypeName);
        Task CreateRawMaterialTypeAsync(RawMaterialType rawMaterialType);
        Task UpdateRawMaterialTypeAsync(RawMaterialType rawMaterialType, RawMaterialType newRawMaterialType);
        Task DeleteRawMaterialTypeAsync(RawMaterialType rawMaterialType);
    }
}
