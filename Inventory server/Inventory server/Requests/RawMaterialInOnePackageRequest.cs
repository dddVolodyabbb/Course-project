using System;

namespace InventoryServer.Context.Requests
{
    public class RawMaterialInOnePackageRequest
    {
        /// <summary>
        /// Тип сырья
        /// </summary>
        public string RawMaterialType { get; set; }
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
        public string LaboratoryAnalysis { get; set; }
        /// <summary>
        /// Склад, в котором хранится сыръё
        /// </summary>
        public string Warehouse { get; set; }
        /// <summary>
        /// Производитель сырья
        /// </summary>
        public string RawMaterialProducer { get; set; }
        /// <summary>
        /// Компания доставки сырья
        /// </summary>
        public string DeliveryCompany { get; set; }
        /// <summary>
        /// Стоймость доставки сырья
        /// </summary>
        public decimal CostOfDelivery { get; set; }
        /// <summary>
        /// Примечание
        /// </summary>
        public string Note { get; set; }
    }
}
