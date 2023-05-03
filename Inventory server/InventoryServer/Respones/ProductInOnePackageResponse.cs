using System;

namespace InventoryServer.Respones
{
    public class ProductInOnePackageResponse
    {
        public int Id { get; set; }
        /// <summary>
        /// Тип продукта
        /// </summary>
        public string ProductType { get; set; }
        /// <summary>
        /// Вес продукции в одной упаковке
        /// </summary>
        public int Weight { get; set; }
        /// <summary>
        /// Себестоймость продукции одной упаковки
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
        /// <summary>
        /// Склад, в котором хранится сыръё
        /// </summary>
        public string Warehouse { get; set; }
        /// <summary>
        /// Лабораторный анализ. false если не пройден.
        /// </summary>
        public string LaboratoryAnalysis { get; set; }
        /// <summary>
        /// Примечание
        /// </summary>
        public string Note { get; set; }
    }
}
