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
		public async Task<RawMaterialType> GetRawMaterialTypeByNameAsync(string name)
		{
			using var dbContextInventoryControl = new ContextInventoryControl();
			return await dbContextInventoryControl.RawMaterialsTypes
				.FirstAsync(d => d.Name == name)
				.ConfigureAwait(false);
        }

        public async Task<RawMaterialType> GetOneRawMaterialTypeAsync(int rawMaterialTypeId)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            return await dbContextInventoryControl.RawMaterialsTypes
                .FirstAsync(d => d.Id == rawMaterialTypeId)
                .ConfigureAwait(false);
        }

        public async Task CreateRawMaterialTypeAsync(RawMaterialType rawMaterialType)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            dbContextInventoryControl.RawMaterialsTypes.Add(rawMaterialType);
            await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateRawMaterialTypeAsync(int id, RawMaterialType newRawMaterialType)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            var rawMaterialType = await dbContextInventoryControl.RawMaterialsTypes
				.FirstAsync(d => d.Id == id)
				.ConfigureAwait(false);
            rawMaterialType.Name = newRawMaterialType.Name;
            await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteRawMaterialTypeAsync(int id)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
			var rawMaterialType = await dbContextInventoryControl.RawMaterialsTypes
				.FirstAsync(d => d.Id == id)
				.ConfigureAwait(false);
            dbContextInventoryControl.RawMaterialsTypes.Remove(rawMaterialType);
            await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
