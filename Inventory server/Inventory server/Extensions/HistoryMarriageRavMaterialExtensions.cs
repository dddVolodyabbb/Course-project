
using System.Data.Entity;
using System.Threading.Tasks;
using InventoryServer.Context.Contexts;
using InventoryServer.Context.Requests;
using InventoryServer.Context.Respones;
using InventoryServer.Domain.Entities;


namespace InventoryServer.Context.Extensions
{
    public static class HistoryMarriageRavMaterialExtensions
    {
        public static HistoryMarriageRavMaterialResponse ToResponse(this HistoryMarriageRavMaterial marriageHistory)
        {
            return new HistoryMarriageRavMaterialResponse
            {
                Id = marriageHistory.Id,
                RavMaterialType = marriageHistory.RawMaterialType.Name,
                Weight = marriageHistory.Weight,
                CostPrice = marriageHistory.CostPrice,
                DateOfAssignment = marriageHistory.DateOfAssignment,
                Note = marriageHistory.Note
            };
        }

        public static async Task<HistoryMarriageRavMaterial> ToEntity(this HistoryMarriageRavMaterialRequest historyMarriageRavMaterialRequest)
        {
            using var db = new ContextInventory();
            return new HistoryMarriageRavMaterial
            {
                RawMaterialType = await db.RawMaterialsTypes
                    .FirstAsync(d => d.Name == historyMarriageRavMaterialRequest.RawMaterialType)
                    .ConfigureAwait(false),
                Weight = historyMarriageRavMaterialRequest.Weight,
                CostPrice = historyMarriageRavMaterialRequest.CostPrice,
                DateOfAssignment = historyMarriageRavMaterialRequest.DateOfAssignment,
                Note = historyMarriageRavMaterialRequest.Note
            };
        }
    }
}
