using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using InventoryServer.Context.Contexts;
using InventoryServer.Domain.Entities;

namespace InventoryServer.Context.Providers.RawMaterialInOnePackages
{
	public class RawMaterialInOnePackageProvider : IRawMaterialInOnePackageProvider
	{
		public async Task<ICollection<RawMaterialInOnePackage>> GetAllRawMaterialInOnePackageAsync()
		{
			using var dbContextInventoryControl = new ContextInventoryControl();
			return await dbContextInventoryControl.RawMaterialInOnePackages.ToListAsync().ConfigureAwait(false);
		}

		public async Task<RawMaterialInOnePackage> GetOneRawMaterialInOnePackageAsync(int rawMaterialInOnePackageName)
		{
			using var dbContextInventoryControl = new ContextInventoryControl();
			return await dbContextInventoryControl.RawMaterialInOnePackages
				.FirstAsync(d => d.Id == rawMaterialInOnePackageName)
				.ConfigureAwait(false);
		}

		public async Task CreateRawMaterialInOnePackageAsync(RawMaterialInOnePackage rawMaterialInOnePackage)
		{
			using var dbContextInventoryControl = new ContextInventoryControl();
			dbContextInventoryControl.RawMaterialInOnePackages.Add(rawMaterialInOnePackage);
			await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
		}

		public async Task UpdateRawMaterialInOnePackageAsync(int id, RawMaterialInOnePackage newRawMaterialInOnePackage)
		{
			using var dbContextInventoryControl = new ContextInventoryControl();
			var rawMaterialInOnePackage = await dbContextInventoryControl.RawMaterialInOnePackages
				.FirstAsync(d => d.Id == id)
				.ConfigureAwait(false);
			rawMaterialInOnePackage.CostOfDelivery = newRawMaterialInOnePackage.CostOfDelivery;
			rawMaterialInOnePackage.DateOfManufacture = newRawMaterialInOnePackage.DateOfManufacture;
            rawMaterialInOnePackage.RawMaterialTypeId = newRawMaterialInOnePackage.RawMaterialTypeId;
            rawMaterialInOnePackage.DeliveryCompanyId = newRawMaterialInOnePackage.DeliveryCompanyId;
            rawMaterialInOnePackage.RawMaterialProducerId = newRawMaterialInOnePackage.RawMaterialProducerId;
            rawMaterialInOnePackage.Suitability = newRawMaterialInOnePackage.Suitability;
            rawMaterialInOnePackage.WarehouseId = newRawMaterialInOnePackage.WarehouseId;
            rawMaterialInOnePackage.LaboratoryAnalysis = newRawMaterialInOnePackage.LaboratoryAnalysis;
            rawMaterialInOnePackage.Price = newRawMaterialInOnePackage.Price;
            rawMaterialInOnePackage.Note = newRawMaterialInOnePackage.Note;
            rawMaterialInOnePackage.SellBy = newRawMaterialInOnePackage.SellBy;
            rawMaterialInOnePackage.Weight = newRawMaterialInOnePackage.Weight;
            await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
		}

		public async Task DeleteRawMaterialInOnePackageAsync(int id)
		{
			using var dbContextInventoryControl = new ContextInventoryControl();
			var rawMaterialInOnePackage = await dbContextInventoryControl.RawMaterialInOnePackages
				.FirstAsync(d => d.Id == id)
				.ConfigureAwait(false);
            dbContextInventoryControl.RawMaterialInOnePackages.Remove(rawMaterialInOnePackage);
			await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
		}
	}
}