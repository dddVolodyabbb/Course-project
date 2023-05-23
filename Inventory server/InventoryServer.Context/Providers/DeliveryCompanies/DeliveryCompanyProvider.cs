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

		public async Task<DeliveryCompany> GetDeliveryCompanyByNameAsync(string name)
		{
			using var dbContextInventoryControl = new ContextInventoryControl();
			return await dbContextInventoryControl.DeliveryCompanies
				.FirstAsync(d => d.Name == name)
				.ConfigureAwait(false);
        }

        public async Task<DeliveryCompany> GetOneDeliveryCompanyAsync(int deliveryCompanyId)
		{
			using var dbContextInventoryControl = new ContextInventoryControl();
			return await dbContextInventoryControl.DeliveryCompanies
				.FirstAsync(d => d.Id == deliveryCompanyId)
				.ConfigureAwait(false);
		}

		public async Task CreateDeliveryCompanyAsync(DeliveryCompany deliveryCompany)
		{
			using var dbContextInventoryControl = new ContextInventoryControl();
			dbContextInventoryControl.DeliveryCompanies.Add(deliveryCompany);
			await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
		}

		public async Task UpdateDeliveryCompanyAsync(int id, DeliveryCompany newDeliveryCompany)
		{
			using var dbContextInventoryControl = new ContextInventoryControl();
			var deliveryCompany = await dbContextInventoryControl.DeliveryCompanies
				.FirstAsync(d => d.Id == id)
				.ConfigureAwait(false);
			deliveryCompany.Name = newDeliveryCompany.Name;
			await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
		}

		public async Task DeleteDeliveryCompanyAsync(int id)
		{
			using var dbContextInventoryControl = new ContextInventoryControl();
			var deliveryCompany = await dbContextInventoryControl.DeliveryCompanies
				.FirstAsync(d => d.Id == id)
				.ConfigureAwait(false);
            dbContextInventoryControl.DeliveryCompanies.Remove(deliveryCompany);
			await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
		}
	}
}