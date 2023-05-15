
using System;

namespace InventoryServer.Requests
{
    /// <summary>
    /// История брака сыръя
    /// </summary>
    public class HistoryDefectiveRavMaterialRequest
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
