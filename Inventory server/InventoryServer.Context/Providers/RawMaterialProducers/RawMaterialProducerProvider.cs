using InventoryServer.Context.Contexts;
using InventoryServer.Domain.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace InventoryServer.Context.Providers.RawMaterialProducers
{
    public class RawMaterialProducerProvider : IRawMaterialProducerProvider
    {
        public async Task<ICollection<RawMaterialProducer>> GetAllRawMaterialProducerAsync()
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            return await dbContextInventoryControl.RawMaterialsProducers.ToListAsync().ConfigureAwait(false);
        }

        public async Task<RawMaterialProducer> GetOneRawMaterialProducerAsync(string rawMaterialProducerName)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            return await dbContextInventoryControl.RawMaterialsProducers
                .FirstAsync(d => d.Name == rawMaterialProducerName)
                .ConfigureAwait(false);
        }

        public async Task CreateRawMaterialProducerAsync(RawMaterialProducer rawMaterialProducer)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            dbContextInventoryControl.RawMaterialsProducers.Add(rawMaterialProducer);
            await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateRawMaterialProducerAsync(RawMaterialProducer rawMaterialProducer, RawMaterialProducer newRawMaterialProducer)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            rawMaterialProducer.Name = newRawMaterialProducer.Name;
            await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteRawMaterialProducerAsync(RawMaterialProducer rawMaterialProducer)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            dbContextInventoryControl.RawMaterialsProducers.Remove(rawMaterialProducer);
            await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
