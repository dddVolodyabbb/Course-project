using InventoryServer.Context.Contexts;
using InventoryServer.Domain.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace InventoryServer.Context.Providers.RawMaterialProducers
{
    public class RawMaterialProducerProvider : IRawMaterialProducerProvider
    {
        public ContextInventoryControl DbContextInventoryControl => new();
        public async Task<ICollection<RawMaterialProducer>> GetAllRawMaterialProducerAsync()
        {
            return await DbContextInventoryControl.RawMaterialsProducers.ToListAsync().ConfigureAwait(false);
        }

        public async Task<RawMaterialProducer> GetOneRawMaterialProducerAsync(string rawMaterialProducerName)
        {
            return await DbContextInventoryControl.RawMaterialsProducers
                .FirstAsync(d => d.Name == rawMaterialProducerName)
                .ConfigureAwait(false);
        }

        public async Task CreateRawMaterialProducerAsync(RawMaterialProducer rawMaterialProducer)
        {
            DbContextInventoryControl.RawMaterialsProducers.Add(rawMaterialProducer);
            await DbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateRawMaterialProducerAsync(RawMaterialProducer rawMaterialProducer, RawMaterialProducer newRawMaterialProducer)
        {
            rawMaterialProducer.Name = newRawMaterialProducer.Name;
            await DbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteRawMaterialProducerAsync(RawMaterialProducer rawMaterialProducer)
        {
            DbContextInventoryControl.RawMaterialsProducers.Remove(rawMaterialProducer);
            await DbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
