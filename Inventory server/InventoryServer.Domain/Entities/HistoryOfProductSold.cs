using System;

namespace InventoryServer.Domain.Entities
{
    /// <summary>
    /// История продажи продукции
    /// </summary>
    public class HistoryOfProductSold
    {
        public int Id { get; set; }
        public int ProductTypeId { get; set; }
        /// <summary>
        /// Тип продукта
        /// </summary>
        public ProductType ProductType { get; set; }
        public int Weight { get; set; }
        /// <summary>
        /// Стоймость продукции одной упаковки
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Дата производства продукции
        /// </summary>
        public DateTime DateOfManufacture { get; set; }
        /// <summary>
        /// Срок годности продукции в днях 
        /// </summary>
        public DateTime Suitability { get; set; }
        /// <summary>
        /// Окончание срока годности продукции
        /// </summary>
    }
}
