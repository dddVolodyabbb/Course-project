using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryServer.Domain.Entities;

namespace InventoryServer.Context.Providers.RawMaterialInOnePackages
{
    public interface IRawMaterialInOnePackageProvider
	{
        Task<ICollection<RawMaterialInOnePackage>> GetAllRawMaterialInOnePackageAsync();
        Task<RawMaterialInOnePackage> GetOneRawMaterialInOnePackageAsync(int rawMaterialInOnePackageName);
        Task CreateRawMaterialInOnePackageAsync(RawMaterialInOnePackage rawMaterialInOnePackage);
        Task UpdateRawMaterialInOnePackageAsync(int id, RawMaterialInOnePackage newRawMaterialInOnePackage);
        Task DeleteRawMaterialInOnePackageAsync(int id);
    }
}