using InventoryServer.Context.Contexts;
using InventoryServer.Domain.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace InventoryServer.Context.Providers.Warehouses
{
    public class WarehouseProvider : IWarehouseProvider
    {
        public async Task<ICollection<Warehouse>> GetAllWarehouseAsync()
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            return await dbContextInventoryControl.Warehouses.ToListAsync().ConfigureAwait(false);
        }

		public async Task<Warehouse> GetWarehouseByNameAsync(string name)
		{
			using var dbContextInventoryControl = new ContextInventoryControl();
			return await dbContextInventoryControl.Warehouses
				.FirstAsync(d => d.Name == name)
				.ConfigureAwait(false);
        }

        public async Task<Warehouse> GetOneWarehouseAsync(int warehouseId)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            return await dbContextInventoryControl.Warehouses
                .FirstAsync(d => d.Id == warehouseId)
                .ConfigureAwait(false);
        }

        public async Task CreateWarehouseAsync(Warehouse warehouse)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            dbContextInventoryControl.Warehouses.Add(warehouse);
            await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateWarehouseAsync(int id, Warehouse newWarehouse)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
			var warehouse = await dbContextInventoryControl.Warehouses
				.FirstAsync(d => d.Id == id)
				.ConfigureAwait(false);
            warehouse.Name = newWarehouse.Name;
            await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteWarehouseAsync(int id)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
			var warehouse = await dbContextInventoryControl.Warehouses
				.FirstAsync(d => d.Id == id)
				.ConfigureAwait(false);
            dbContextInventoryControl.Warehouses.Remove(warehouse);
            await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
