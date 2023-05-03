using InventoryServer.Context.Contexts;
using InventoryServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryServer.Context.Providers.RawMaterialProducers
{
    public interface IRawMaterialProducerProvider
    {
        public ContextInventoryControl DbContextInventoryControl { get; }
        Task<ICollection<RawMaterialProducer>> GetAllRawMaterialProducerAsync();
        Task<RawMaterialProducer> GetOneRawMaterialProducerAsync(string rawMaterialProducerName);
        Task CreateRawMaterialProducerAsync(RawMaterialProducer rawMaterialProducer);
        Task UpdateRawMaterialProducerAsync(RawMaterialProducer rawMaterialProducer, RawMaterialProducer newRawMaterialProducer);
        Task DeleteRawMaterialProducerAsync(RawMaterialProducer rawMaterialProducer);
    }
}
