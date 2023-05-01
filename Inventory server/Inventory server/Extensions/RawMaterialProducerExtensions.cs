
using InventoryServer.Context.Requests;
using InventoryServer.Context.Respones;
using InventoryServer.Domain.Entities;

namespace InventoryServer.Context.Extensions
{
    public static class RawMaterialProducerExtensions
    {
        public static RawMaterialProducerResponse ToResponse(this RawMaterialProducer rawMaterialProducer)
        {
            return new RawMaterialProducerResponse
            {
                Id = rawMaterialProducer.Id,
                Name = rawMaterialProducer.Name,
            };
        }

        public static RawMaterialProducer ToEntity(this RawMaterialProducerRequest rawMaterialProducerRequest)
        {
            return new RawMaterialProducer
            {
                Name = rawMaterialProducerRequest.Name
            };
        }
    }
}
