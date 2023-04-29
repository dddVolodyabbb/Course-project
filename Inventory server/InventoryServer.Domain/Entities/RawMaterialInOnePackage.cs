using System;

namespace InventoryServer.Domain.Entities
{
    /// <summary>
    /// Информация о сырье в одной упаковке
    /// </summary>
    public class RawMaterialInOnePackage
    {
        public int Id { get; set; }
        public int RawMaterialTypeId { get; set; }
        /// <summary>
        /// Тип сырья
        /// </summary>
        public RawMaterialType RawMaterialType { get; set; }
        /// <summary>
        /// Стоймость за один мешок
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Вес сырья в граммах в одном мешке
        /// </summary>
        public int Weight { get; set; }
        /// <summary>
        /// Дата производства сырья
        /// </summary>
        public DateTime DateOfManufacture { get; set; }
        /// <summary>
        /// Срок годности сырья в днях
        /// </summary>
        public DateTime Suitability { get; set; }
        /// <summary>
        /// Окончание срока годности сырья
        /// </summary>
        public DateTime SellBy { get; set; }
        /// <summary>
        /// Лабораторный анализ. false если не пройден.
        /// </summary>
        public bool LaboratoryAnalysis { get; set; }
        public int WarehouseId { get; set; }
        /// <summary>
        /// Склад, в котором хранится сыръё
        /// </summary>
        public Warehouse Warehouse { get; set; }
        public int RawMaterialProducerId { get; set; }
        /// <summary>
        /// Производитель сырья
        /// </summary>
        public RawMaterialProducer RawMaterialProducer { get; set; }
        public int DeliveryCompanyId { get; set; }
        /// <summary>
        /// Компания доставки сырья
        /// </summary>
        public DeliveryCompany DeliveryCompany { get; set; }
        /// <summary>
        /// Стоймость доставки сырья
        /// </summary>
        public decimal CostOfDelivery { get; set; }
    }
}
