using InventoryServer.Context.Contexts;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using InventoryServer.Domain.Entities;

namespace InventoryServer.Context.Providers.DeliveryCompanies
{
    public class DeliveryCompanyProvider : IDeliveryCompanyProvider
    {
        public async Task<ICollection<DeliveryCompany>> GetAllDeliveryCompanyAsync()
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            return await dbContextInventoryControl.DeliveryCompanies.ToListAsync().ConfigureAwait(false);
        }

        public async Task<DeliveryCompany> GetOneDeliveryCompanyAsync(string deliveryCompanyName)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            return await dbContextInventoryControl.DeliveryCompanies
                .FirstAsync(d => d.Name == deliveryCompanyName)
                .ConfigureAwait(false);
        }

        public async Task CreateDeliveryCompanyAsync(DeliveryCompany deliveryCompany)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            dbContextInventoryControl.DeliveryCompanies.Add(deliveryCompany);
            await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateDeliveryCompanyAsync(DeliveryCompany deliveryCompany, DeliveryCompany newDeliveryCompany)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            deliveryCompany.Name = newDeliveryCompany.Name;
            await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteDeliveryCompanyAsync(DeliveryCompany deliveryCompany)
        {
            using var dbContextInventoryControl = new ContextInventoryControl();
            dbContextInventoryControl.DeliveryCompanies.Remove(deliveryCompany);
            await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
