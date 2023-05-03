
namespace InventoryServer.Requests
{
    public class PercentageOfRawMaterialRequest
    {
        /// <summary>
        /// Тип продукта
        /// </summary>
        public string ProductType { get; set; }
        /// <summary>
        /// Тип сырья
        /// </summary>
        public string RawMaterialType { get; set; }
        /// <summary>
        /// Процентное отношение сырья к общей массе продукта
        /// </summary>
        public int Percent { get; set; }
    }
}
