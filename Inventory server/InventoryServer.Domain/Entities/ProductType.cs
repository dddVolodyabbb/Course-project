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
        public ICollection<ProductInOnePackage> Products { get; set; }
        public ICollection<ProductRecipe> Recipes { get; set; }
        public ICollection<PercentageOfRawMaterials> PercentageOfRawMaterials { get; set; }
        public ICollection<HistoryOfProductSold> HistoryOfProductSold { get; set;}
    }
}
