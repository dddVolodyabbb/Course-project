using System.Collections.Generic;

namespace InventoryServer.Domain.Entities
{
	/// <summary>
	/// Склад
	/// </summary>
	public class Warehouse
	{
		public int Id { get; set; }

		/// <summary>
		/// Название склада
		/// </summary>
		public string Name { get; set; }

		public ICollection<RawMaterialInOnePackage> RawMaterialInOnePackage { get; set; }
		public ICollection<ProductInOnePackage> ProductInOnePackage { get; set; }
	}
}