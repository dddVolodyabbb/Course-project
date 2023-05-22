using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using InventoryServer.Context.Contexts;
using InventoryServer.Domain.Entities;

namespace InventoryServer.Context.Providers.PercentageOfRawMaterials
{
	public class PercentageOfRawMaterialProvider : IPercentageOfRawMaterialProvider
	{
		public async Task<ICollection<PercentageOfRawMaterial>> GetAllPercentageOfRawMaterialAsync()
		{
			using var dbContextInventoryControl = new ContextInventoryControl();
			return await dbContextInventoryControl.PercentOfRawMaterials.ToListAsync().ConfigureAwait(false);
		}

		public async Task<PercentageOfRawMaterial> GetOnePercentageOfRawMaterialAsync(int percentageOfRawMaterialName)
		{
			using var dbContextInventoryControl = new ContextInventoryControl();
			return await dbContextInventoryControl.PercentOfRawMaterials
				.FirstAsync(d => d.Id == percentageOfRawMaterialName)
				.ConfigureAwait(false);
		}

		public async Task CreatePercentageOfRawMaterialAsync(PercentageOfRawMaterial percentageOfRawMaterial)
		{
			using var dbContextInventoryControl = new ContextInventoryControl();
			dbContextInventoryControl.PercentOfRawMaterials.Add(percentageOfRawMaterial);
			await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
		}

		public async Task UpdatePercentageOfRawMaterialAsync(int id, PercentageOfRawMaterial newPercentageOfRawMaterial)
		{
			using var dbContextInventoryControl = new ContextInventoryControl();
			var percentageOfRawMaterial = await dbContextInventoryControl.PercentOfRawMaterials
				.FirstAsync(d => d.Id == id)
				.ConfigureAwait(false);
			percentageOfRawMaterial.Percent = newPercentageOfRawMaterial.Percent;
			percentageOfRawMaterial.ProductTypeId = newPercentageOfRawMaterial.ProductTypeId;
            percentageOfRawMaterial.RawMaterialTypeId = newPercentageOfRawMaterial.RawMaterialTypeId;
            await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
		}

		public async Task DeletePercentageOfRawMaterialAsync(int id)
		{
			using var dbContextInventoryControl = new ContextInventoryControl();
			var percentageOfRawMaterial = await dbContextInventoryControl.PercentOfRawMaterials
				.FirstAsync(d => d.Id == id)
				.ConfigureAwait(false);
            dbContextInventoryControl.PercentOfRawMaterials.Remove(percentageOfRawMaterial);
			await dbContextInventoryControl.SaveChangesAsync().ConfigureAwait(false);
		}
	}
}