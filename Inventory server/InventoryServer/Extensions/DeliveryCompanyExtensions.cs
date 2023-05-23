using InventoryServer.Domain.Entities;
using InventoryServer.Requests;
using InventoryServer.Respones;

namespace InventoryServer.Extensions
{
    public static class DeliveryCompanyExtensions
    {
        public static DeliveryCompanyResponse ToResponse(this DeliveryCompany deliveryCompany)
        {
            return new DeliveryCompanyResponse
            {
                Id = deliveryCompany.Id,
                Name = deliveryCompany.Name
            };
        }

        public static DeliveryCompany ToEntity(this DeliveryCompanyRequest deliveryCompanyRequest)
        {
            return new DeliveryCompany()
            {
                Name = deliveryCompanyRequest.Name
            };
        }
    }
}
