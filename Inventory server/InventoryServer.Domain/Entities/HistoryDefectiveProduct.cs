using System;

namespace InventoryServer.Domain.Entities
{
	/// <summary>
	/// История брака продукции
	/// </summary>
	public class HistoryDefectiveProduct
	{
		public int Id { get; set; }
		public int ProductTypeId { get; set; }

		/// <summary>
		/// Бракованная продукция
		/// </summary>
		public ProductType ProductType { get; set; }

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