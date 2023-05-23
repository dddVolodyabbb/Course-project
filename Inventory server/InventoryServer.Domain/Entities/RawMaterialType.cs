using System.Collections.Generic;

namespace InventoryServer.Domain.Entities
{
	/// <summary>
	/// Тип сырья
	/// </summary>
	public class RawMaterialType
	{
		public int Id { get; set; }

		/// <summary>
		/// Название типа сырья
		/// </summary>
		public string Name { get; set; }

		public ICollection<RawMaterialInOnePackage> RawMaterialInOnePackage { get; set; }
		public ICollection<PercentageOfRawMaterial> PercentOfRawMaterials { get; set; }
		public ICollection<HistoryDefectiveRavMaterial> HistoryMarriageRavMaterials { get; set; }
	}
}