using System;


namespace InventoryServer.Context.Respones
{
    public class HistoryOfProductSoldResponse
    {
        public int Id { get; set; }
        /// <summary>
        /// Тип продукта
        /// </summary>
        public string ProductType { get; set; }
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
        public DateTime SellBy { get; set; }
    }
}
