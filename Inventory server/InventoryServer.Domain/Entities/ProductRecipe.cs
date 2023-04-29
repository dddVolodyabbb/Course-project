namespace InventoryServer.Domain.Entities
{
    /// <summary>
    /// Рецепт продукции \ процентное соотношение сырья
    /// </summary>
    public class ProductRecipe
    {
        public int Id { get; set; }
        public int ProductTypeId { get; set; }
        /// <summary>
        /// Тип продукта
        /// </summary>
        public ProductType ProductType { get; set; }
    }
}
