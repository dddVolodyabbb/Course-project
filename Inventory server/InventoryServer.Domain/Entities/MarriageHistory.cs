using System;

namespace InventoryServer.Domain.Entities
{
    /// <summary>
    /// История брака
    /// </summary>
    public class MarriageHistory
    {
        public int Id { get; set; }
        /// <summary>
        /// Бракованная продукция/сырьё
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Вес в граммах
        /// </summary>
        public int Weight { get; set; }
        public decimal CostPrice { get; set; }
        /// <summary>
        /// Дата списания в брак
        /// </summary>
        public DateTime DateOfAssignment { get; set; }
    }
}
