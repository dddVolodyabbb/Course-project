using System;

namespace InventoryServer.Domain.Entities
{
	/// <summary>
	/// История брака сыръя
	/// </summary>
	public class HistoryDefectiveRavMaterial
	{
		public int Id { get; set; }
		public int RawMaterialTypeId { get; set; }

		/// <summary>
		/// Бракованное сырьё
		/// </summary>
		public RawMaterialType RawMaterialType { get; set; }

		/// <summary>
		/// Вес в граммах
		/// </summary>
		public int Weight { get; set; }

		/// <summary>
		/// Себестоймость брака
		/// </summary>
		public decimal CostPrice { get; set; }

		/// <summary>
		/// Дата списания в брак
		/// </summary>
		public DateTime DateOfAssignment { get; set; }

		/// <summary>
		/// Примечание
		/// </summary>
		public string Note { get; set; }
	}
}