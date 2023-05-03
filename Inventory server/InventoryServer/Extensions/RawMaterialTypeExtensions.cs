using InventoryServer.Domain.Entities;
using InventoryServer.Requests;
using InventoryServer.Respones;

namespace InventoryServer.Extensions
{
    public static class RawMaterialTypeExtensions
    {
        public static RawMaterialTypeResponse ToResponse(RawMaterialType rawMaterialType)
        {
            return new RawMaterialTypeResponse
            {
                Id = rawMaterialType.Id,
                Name = rawMaterialType.Name
            };
        }

        public static RawMaterialType ToEntity(this RawMaterialTypeRequest rawMaterialTypeResponse)
        {
            return new RawMaterialType
            {
                Name = rawMaterialTypeResponse.Name
            };
        }
    }
}
