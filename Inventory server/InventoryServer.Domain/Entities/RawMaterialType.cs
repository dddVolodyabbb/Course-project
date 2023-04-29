using System.Collections.Generic;

namespace InventoryServer.Domain.Entities
{
    /// <summary>
    /// Тип сырья
    /// </summary>
    public class RawMaterialType
    {
        public int Id { get; set; }
        /// <summary>
        /// Название типа сырья
        /// </summary>
        public string Name { get; set; }
        public ICollection<RawMaterialInOnePackage> RawMaterials { get; set; }
        public ICollection<PercentageOfRawMaterials> PercentOfRawMaterials { get; set;}
    }
}
