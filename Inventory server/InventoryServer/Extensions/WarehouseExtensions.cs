using InventoryServer.Domain.Entities;
using InventoryServer.Requests;
using InventoryServer.Respones;

namespace InventoryServer.Extensions
{
    public static class WarehouseExtensions
    {
        public static WarehouseResponse ToResponse(this Warehouse warehouse)
        {
            return new WarehouseResponse
            {
                Id = warehouse.Id,
                Name = warehouse.Name
            };
        }

        public static Warehouse ToEntity(this WarehouseRequest warehouse)
        {
            return new Warehouse
            {
                Name = warehouse.Name
            };
        }
    }
}
