using InventoryServer.Context.Contexts;
using InventoryServer.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryServer.Context.Providers.DeliveryCompanies
{
    public interface IDeliveryCompanyProvider
    {
        Task<ICollection<DeliveryCompany>> GetAllDeliveryCompanyAsync();
        Task<DeliveryCompany> GetOneDeliveryCompanyAsync(string deliveryCompanyName);
        Task CreateDeliveryCompanyAsync(DeliveryCompany deliveryCompany);
        Task UpdateDeliveryCompanyAsync(DeliveryCompany deliveryCompany, DeliveryCompany newDeliveryCompany);
        Task DeleteDeliveryCompanyAsync(DeliveryCompany deliveryCompany);
    }
}
