using InventoryServer.Context.Contexts;
using InventoryServer.Domain.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace InventoryServer.Context.Providers.Warehouses
{
    public class WarehouseProvider : IWarehouseProvider
    {
        public ContextInventoryControl DbContextInventoryControl => new();
        public async Task<ICollection<Warehouse>> GetAllWarehouseAsync()
        {
            return await DbContextInventoryControl.Warehouses.ToListAsync().ConfigureAwait(false);
        }

        public async Task<Warehouse> GetOneWarehouseAsync(string warehouseName)
        {
            return await DbContextInventoryControl.Warehouses
                .FirstAsync(d => d.Name == warehouseName)
                .ConfigureAwait(false);
        }

        public async Task CreateWarehouseAsync(Warehouse warehouse)
        {
            DbContextInventoryControl.Warehouses.Add(warehouse);
            await DbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateWarehouseAsync(Warehouse warehouse, Warehouse newWarehouse)
        {
            warehouse.Name = newWarehouse.Name;
            await DbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteWarehouseAsync(Warehouse warehouse)
        {
            DbContextInventoryControl.Warehouses.Remove(warehouse);
            await DbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
