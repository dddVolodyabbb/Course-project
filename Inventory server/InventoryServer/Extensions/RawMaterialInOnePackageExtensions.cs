using System.Threading.Tasks;
using InventoryServer.Context.Providers.DeliveryCompanies;
using InventoryServer.Context.Providers.RawMaterialProducers;
using InventoryServer.Context.Providers.RawMaterialTypes;
using InventoryServer.Context.Providers.Warehouses;
using InventoryServer.Domain.Entities;
using InventoryServer.Requests;
using InventoryServer.Respones;

namespace InventoryServer.Extensions
{
	public static class RawMaterialInOnePackageExtensions
	{
		public static RawMaterialInOnePackageResponse ToResponse(this RawMaterialInOnePackage rawMaterialInOnePackage)
		{
			return new RawMaterialInOnePackageResponse
			{
				Id = rawMaterialInOnePackage.Id,
				CostOfDelivery = rawMaterialInOnePackage.CostOfDelivery,
				DateOfManufacture = rawMaterialInOnePackage.DateOfManufacture,
				DeliveryCompany = rawMaterialInOnePackage.DeliveryCompany.Name,
				LaboratoryAnalysis = rawMaterialInOnePackage.LaboratoryAnalysis,
				Note = rawMaterialInOnePackage.Note,
				Price = rawMaterialInOnePackage.Price,
				RawMaterialProducer = rawMaterialInOnePackage.RawMaterialProducer.Name,
				RawMaterialType = rawMaterialInOnePackage.RawMaterialType.Name,
				SellBy = rawMaterialInOnePackage.SellBy,
				Weight = rawMaterialInOnePackage.Weight,
				Suitability = rawMaterialInOnePackage.Suitability,
				Warehouse = rawMaterialInOnePackage.Warehouse.Name
			};
		}

		public static async Task<RawMaterialInOnePackage> ToEntity(this RawMaterialInOnePackageRequest rawMaterialInOnePackageResponse)
		{
			return new RawMaterialInOnePackage
			{
				CostOfDelivery = rawMaterialInOnePackageResponse.CostOfDelivery,
				DateOfManufacture = rawMaterialInOnePackageResponse.DateOfManufacture,
				LaboratoryAnalysis = rawMaterialInOnePackageResponse.LaboratoryAnalysis,
				Note = rawMaterialInOnePackageResponse.Note,
				Price = rawMaterialInOnePackageResponse.Price,
				SellBy = rawMaterialInOnePackageResponse.SellBy,
				Weight = rawMaterialInOnePackageResponse.Weight,
				Suitability = rawMaterialInOnePackageResponse.Suitability,
				Warehouse = await new WarehouseProvider()
					.GetWarehouseByNameAsync(rawMaterialInOnePackageResponse.Warehouse)
					.ConfigureAwait(false),
				DeliveryCompany = await new DeliveryCompanyProvider()
					.GetDeliveryCompanyByNameAsync(rawMaterialInOnePackageResponse.DeliveryCompany)
					.ConfigureAwait(false),
				RawMaterialProducer = await new RawMaterialProducerProvider()
					.GetRawMaterialProducerByNameAsync(rawMaterialInOnePackageResponse.RawMaterialProducer)
					.ConfigureAwait(false),
				RawMaterialType = await new RawMaterialTypeProvider()
					.GetRawMaterialTypeByNameAsync(rawMaterialInOnePackageResponse.RawMaterialType)
					.ConfigureAwait(false),
			};
		}
	}
}