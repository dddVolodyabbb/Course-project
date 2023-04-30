using System.Collections.Generic;

namespace InventoryServer.Domain.Entities
{
    /// <summary>
    /// Прцент сыръя в продукции
    /// </summary>
    public class PercentageOfRawMaterial
    {
        public int Id { get; set; }
        public int ProductTypeId { get; set; }
        /// <summary>
        /// Тип продукта
        /// </summary>
        public ProductType ProductType { get; set; }
        public int RawMaterialTypeId { get; set; }
        /// <summary>
        /// Тип сырья
        /// </summary>
        public RawMaterialType RawMaterialType { get; set; }
        /// <summary>
        /// Процентное отношение сырья к общей массе продукта
        /// </summary>
        public int Percent { get; set; }
    }
}
