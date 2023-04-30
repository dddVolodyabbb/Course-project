using System.Collections.Generic;

namespace InventoryServer.Domain.Entities
{
    /// <summary>
    /// Типы продукции
    /// </summary>
    public class ProductType
    {
        public int Id { get; set; }
        /// <summary>
        /// Название продукции
        /// </summary>
        public string Name { get; set; }
        public ICollection<ProductInOnePackage> ProductInOnePackage { get; set; }
        public ICollection<PercentageOfRawMaterial> PercentageOfRawMaterials { get; set; }
        public ICollection<HistoryOfProductSold> HistoryOfProductSales { get; set;}
    }
}
