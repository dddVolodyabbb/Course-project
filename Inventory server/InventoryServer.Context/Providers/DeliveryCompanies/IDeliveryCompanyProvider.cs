using InventoryServer.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryServer.Context.Providers.DeliveryCompanies
{
	public interface IDeliveryCompanyProvider
	{
		Task<ICollection<DeliveryCompany>> GetAllDeliveryCompanyAsync();
		Task<DeliveryCompany> GetOneDeliveryCompanyAsync(int deliveryCompanyId);
		Task CreateDeliveryCompanyAsync(DeliveryCompany deliveryCompany);
		Task UpdateDeliveryCompanyAsync(int id, DeliveryCompany newDeliveryCompany);
		Task DeleteDeliveryCompanyAsync(int id);
	}
}