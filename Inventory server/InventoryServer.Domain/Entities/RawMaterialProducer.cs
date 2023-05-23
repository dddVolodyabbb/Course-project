using System.Collections.Generic;

namespace InventoryServer.Domain.Entities
{
	/// <summary>
	/// Производитель сырья
	/// </summary>
	public class RawMaterialProducer
	{
		public int Id { get; set; }

		/// <summary>
		/// Название производителя сырья
		/// </summary>
		public string Name { get; set; }

		public ICollection<RawMaterialInOnePackage> RawMaterialInOnePackage { get; set; }
	}
}