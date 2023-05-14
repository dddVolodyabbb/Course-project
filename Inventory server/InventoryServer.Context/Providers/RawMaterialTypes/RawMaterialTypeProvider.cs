using InventoryServer.Context.Contexts;
using InventoryServer.Domain.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace InventoryServer.Context.Providers.RawMaterialTypes
{
    public class RawMaterialTypeProvider : IRawMaterialTypeProvider
    {
        public async Task<ICollection<RawMaterialType>> GetAllRawMaterialTypeAsync()
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            return await dbContextInventoryControl.RawMaterialsTypes.ToListAsync().ConfigureAwait(false);
        }

        public async Task<RawMaterialType> GetOneRawMaterialTypeAsync(string rawMaterialTypeName)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            return await dbContextInventoryControl.RawMaterialsTypes
                .FirstAsync(d => d.Name == rawMaterialTypeName)
                .ConfigureAwait(false);
        }

        public async Task CreateRawMaterialTypeAsync(RawMaterialType rawMaterialType)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            dbContextInventoryControl.RawMaterialsTypes.Add(rawMaterialType);
            await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateRawMaterialTypeAsync(RawMaterialType rawMaterialType, RawMaterialType newRawMaterialType)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            rawMaterialType.Name = newRawMaterialType.Name;
            await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteRawMaterialTypeAsync(RawMaterialType rawMaterialType)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            dbContextInventoryControl.RawMaterialsTypes.Remove(rawMaterialType);
            await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
