
using InventoryServer.Context.Requests;
using InventoryServer.Domain.Entities;
using InventoryServer.Domain.Respones;

namespace InventoryServer.Domain.Extensions
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
