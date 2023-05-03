using InventoryServer.Context.Contexts;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using InventoryServer.Domain.Entities;

namespace InventoryServer.Context.Providers.DeliveryCompanies
{
    public class DeliveryCompanyProvider : IDeliveryCompanyProvider
    {
        public ContextInventoryControl DbContextInventoryControl => new();

        public async Task<ICollection<DeliveryCompany>> GetAllDeliveryCompanyAsync()
        {
            return await DbContextInventoryControl.DeliveryCompanies.ToListAsync().ConfigureAwait(false);
        }

        public async Task<DeliveryCompany> GetOneDeliveryCompanyAsync(string deliveryCompanyName)
        {
            return await DbContextInventoryControl.DeliveryCompanies
                .FirstAsync(d => d.Name == deliveryCompanyName)
                .ConfigureAwait(false);
        }

        public async Task CreateDeliveryCompanyAsync(DeliveryCompany deliveryCompany)
        {
            DbContextInventoryControl.DeliveryCompanies.Add(deliveryCompany);
            await DbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateDeliveryCompanyAsync(DeliveryCompany deliveryCompany, DeliveryCompany newDeliveryCompany)
        {
            deliveryCompany.Name = newDeliveryCompany.Name;
            await DbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteDeliveryCompanyAsync(DeliveryCompany deliveryCompany)
        {
            DbContextInventoryControl.DeliveryCompanies.Remove(deliveryCompany);
            await DbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
