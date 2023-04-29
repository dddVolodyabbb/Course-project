using System.Collections.Generic;

namespace InventoryServer.Domain.Entities
{
    /// <summary>
    /// Компания доставки
    /// </summary>
    public class DeliveryCompany
    {
        public int Id { get; set; }
        /// <summary>
        /// Название компании доставки
        /// </summary>
        public string Name { get; set; }
        public  ICollection<RawMaterialInOnePackage> RawMaterials { get; set; }
    }
}
