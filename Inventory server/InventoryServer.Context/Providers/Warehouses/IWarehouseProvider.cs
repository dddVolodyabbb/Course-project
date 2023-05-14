using InventoryServer.Context.Contexts;
using InventoryServer.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryServer.Context.Providers.Warehouses
{
    public interface IWarehouseProvider
    {
        Task<ICollection<Warehouse>> GetAllWarehouseAsync();
        Task<Warehouse> GetOneWarehouseAsync(string warehouseName);
        Task CreateWarehouseAsync(Warehouse warehouse);
        Task UpdateWarehouseAsync(Warehouse warehouse, Warehouse newWarehouse);
        Task DeleteWarehouseAsync(Warehouse warehouse);
    }
}
