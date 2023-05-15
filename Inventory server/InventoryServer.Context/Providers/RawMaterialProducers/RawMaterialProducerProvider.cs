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

		public async Task<RawMaterialProducer> GetRawMaterialProducerByNameAsync(string name)
		{
			using var dbContextInventoryControl = new ContextInventoryControl();
			return await dbContextInventoryControl.RawMaterialsProducers
				.FirstAsync(d => d.Name == name)
				.ConfigureAwait(false);
        }

        public async Task<RawMaterialProducer> GetOneRawMaterialProducerAsync(int rawMaterialProducerId)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            return await dbContextInventoryControl.RawMaterialsProducers
                .FirstAsync(d => d.Id == rawMaterialProducerId)
                .ConfigureAwait(false);
        }

        public async Task CreateRawMaterialProducerAsync(RawMaterialProducer rawMaterialProducer)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            dbContextInventoryControl.RawMaterialsProducers.Add(rawMaterialProducer);
            await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateRawMaterialProducerAsync(int id, RawMaterialProducer newRawMaterialProducer)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            var rawMaterialProducer = await dbContextInventoryControl.RawMaterialsProducers
				.FirstAsync(d => d.Id == id)
				.ConfigureAwait(false);
            rawMaterialProducer.Name = newRawMaterialProducer.Name;
            await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteRawMaterialProducerAsync(int id)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
			var rawMaterialProducer = await dbContextInventoryControl.RawMaterialsProducers
				.FirstAsync(d => d.Id == id)
				.ConfigureAwait(false);
            dbContextInventoryControl.RawMaterialsProducers.Remove(rawMaterialProducer);
            await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
