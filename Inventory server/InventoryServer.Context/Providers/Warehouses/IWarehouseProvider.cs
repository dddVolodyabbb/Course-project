using InventoryServer.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryServer.Context.Providers.Warehouses
{
	public interface IWarehouseProvider
	{
		Task<ICollection<Warehouse>> GetAllWarehouseAsync();
		Task<Warehouse> GetWarehouseByNameAsync(string name);
		Task<Warehouse> GetOneWarehouseAsync(int warehouseId);
		Task CreateWarehouseAsync(Warehouse warehouse);
		Task UpdateWarehouseAsync(int id, Warehouse newWarehouse);
		Task DeleteWarehouseAsync(int id);
	}
}