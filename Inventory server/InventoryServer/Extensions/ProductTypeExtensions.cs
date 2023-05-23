using InventoryServer.Domain.Entities;
using InventoryServer.Requests;
using InventoryServer.Respones;

namespace InventoryServer.Extensions
{
    public static class ProductTypeExtensions
    {
        public static ProductTypeResponse ToResponse(this ProductType productType)
        {
            return new ProductTypeResponse
            {
                Id = productType.Id,
                Name = productType.Name
            };
        }

        public static ProductType ToEntity(this ProductTypeRequest productTypeRequest)
        {
            return new ProductType
            {
                Name = productTypeRequest.Name
            };
        }
    }
}
