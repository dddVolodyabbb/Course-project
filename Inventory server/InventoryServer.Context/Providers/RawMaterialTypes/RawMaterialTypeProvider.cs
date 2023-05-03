using InventoryServer.Context.Contexts;
using InventoryServer.Domain.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace InventoryServer.Context.Providers.RawMaterialTypes
{
    public class RawMaterialTypeProvider : IRawMaterialTypeProvider
    {
        public ContextInventoryControl DbContextInventoryControl => new();
        public async Task<ICollection<RawMaterialType>> GetAllRawMaterialTypeAsync()
        {
            return await DbContextInventoryControl.RawMaterialsTypes.ToListAsync().ConfigureAwait(false);
        }

        public async Task<RawMaterialType> GetOneRawMaterialTypeAsync(string rawMaterialTypeName)
        {
            return await DbContextInventoryControl.RawMaterialsTypes
                .FirstAsync(d => d.Name == rawMaterialTypeName)
                .ConfigureAwait(false);
        }

        public async Task CreateRawMaterialTypeAsync(RawMaterialType rawMaterialType)
        {
            DbContextInventoryControl.RawMaterialsTypes.Add(rawMaterialType);
            await DbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateRawMaterialTypeAsync(RawMaterialType rawMaterialType, RawMaterialType newRawMaterialType)
        {
            rawMaterialType.Name = newRawMaterialType.Name;
            await DbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteRawMaterialTypeAsync(RawMaterialType rawMaterialType)
        {
            DbContextInventoryControl.RawMaterialsTypes.Remove(rawMaterialType);
            await DbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
