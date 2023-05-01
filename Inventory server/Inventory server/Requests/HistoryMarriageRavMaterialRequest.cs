
using System;

namespace InventoryServer.Context.Requests
{
    /// <summary>
    /// История брака сыръя
    /// </summary>
    public class HistoryMarriageRavMaterialRequest
    {
        /// <summary>
        /// Бракованная продукция/сырьё
        /// </summary>
        public string RawMaterialType { get; set; }
        /// <summary>
        /// Вес в граммах
        /// </summary>
        public int Weight { get; set; }
        /// <summary>
        /// Себестоймость брака
        /// </summary>
        public decimal CostPrice { get; set; }
        /// <summary>
        /// Дата списания в брак
        /// </summary>
        public DateTime DateOfAssignment { get; set; }
        /// <summary>
        /// Примечание
        /// </summary>
        public string Note { get; set; }
    }
}
