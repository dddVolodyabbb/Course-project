using InventoryServer.Context.Requests;
using System.Threading.Tasks;
using InventoryServer.Domain.Extensions;
using InventoryServer.Context.Contexts;

namespace InventoryServer.Context.Commands.Warehouse
{
    public class Create
    {
        public async Task HandleAsync(WarehouseRequests warehouseRequests)
        {
            using (var db = new ContextInventory())
            {
                var warehouse = warehouseRequests.ToEntity();
                db.Warehouses.Add(warehouse);
                await db.SaveChangesAsync().ConfigureAwait(false);
            }
        }
    }
}
