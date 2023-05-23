
using System.Threading.Tasks;
using InventoryServer.Context.Providers.RawMaterialTypes;
using InventoryServer.Domain.Entities;
using InventoryServer.Requests;
using InventoryServer.Respones;

namespace InventoryServer.Extensions
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
            return new HistoryMarriageRavMaterial
            {
                Weight = historyMarriageRavMaterialRequest.Weight,
                CostPrice = historyMarriageRavMaterialRequest.CostPrice,
                DateOfAssignment = historyMarriageRavMaterialRequest.DateOfAssignment,
                Note = historyMarriageRavMaterialRequest.Note,
                RawMaterialType = await new RawMaterialTypeProvider()
                    .GetOneRawMaterialTypeAsync(historyMarriageRavMaterialRequest.RawMaterialType)
                    .ConfigureAwait(false),
            };
        }
    }
}
