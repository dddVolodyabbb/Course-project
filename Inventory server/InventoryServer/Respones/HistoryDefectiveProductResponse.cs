using System;

namespace InventoryServer.Respones
{
	public class HistoryDefectiveProductResponse
	{
		public int Id { get; set; }

		/// <summary>
		/// Бракованная продукция
		/// </summary>
		public string ProductType { get; set; }

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