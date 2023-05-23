
using InventoryServer.Domain.Entities;

using System.Collections.Generic;

using System.Threading.Tasks;

namespace InventoryServer.Context.Providers.RawMaterialProducers
{
    public interface IRawMaterialProducerProvider
    {
        Task<ICollection<RawMaterialProducer>> GetAllRawMaterialProducerAsync();
		Task<RawMaterialProducer> GetRawMaterialProducerByNameAsync(string name);
        Task<RawMaterialProducer> GetOneRawMaterialProducerAsync(int rawMaterialProducerId);
        Task CreateRawMaterialProducerAsync(RawMaterialProducer rawMaterialProducer);
        Task UpdateRawMaterialProducerAsync(int id, RawMaterialProducer newRawMaterialProducer);
        Task DeleteRawMaterialProducerAsync(int id);
    }
}
