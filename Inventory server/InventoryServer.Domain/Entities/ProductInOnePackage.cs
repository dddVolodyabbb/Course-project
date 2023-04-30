using System;

namespace InventoryServer.Domain.Entities
{
    /// <summary>
    /// Информация о продукте в одной упаковке
    /// </summary>
    public class ProductInOnePackage
    {
        public int Id { get; set; }
        public int ProductTypeId { get; set; }
        /// <summary>
        /// Тип продукта
        /// </summary>
        public ProductType ProductType { get; set; }
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
        /// Лабораторный анализ. false если не пройден.
        /// </summary>
        public int WarehouseId { get; set; }
        /// <summary>
        /// Склад, в котором хранится сыръё
        /// </summary>
        public Warehouse Warehouse { get; set; }
        public bool LaboratoryAnalysis { get; set; }
        public string Note { get; set; }
    }
}
