using System.Threading.Tasks;
using InventoryServer.Context.Providers.RawMaterialTypes;
using InventoryServer.Domain.Entities;
using InventoryServer.Requests;
using InventoryServer.Respones;

namespace InventoryServer.Extensions
{
    public static class HistoryDefectiveRavMaterialExtensions
    {
        public static HistoryDefectiveRavMaterialResponse ToResponse(this HistoryDefectiveRavMaterial defectiveHistory)
        {
            return new HistoryDefectiveRavMaterialResponse
            {
                Id = defectiveHistory.Id,
                RavMaterialType = defectiveHistory.RawMaterialType.Name,
                Weight = defectiveHistory.Weight,
                CostPrice = defectiveHistory.CostPrice,
                DateOfAssignment = defectiveHistory.DateOfAssignment,
                Note = defectiveHistory.Note
            };
        }

        public static async Task<HistoryDefectiveRavMaterial> ToEntity(
            this HistoryDefectiveRavMaterialRequest historyDefectiveRavMaterialRequest)
        {
            return new HistoryDefectiveRavMaterial
            {
                Weight = historyDefectiveRavMaterialRequest.Weight,
                CostPrice = historyDefectiveRavMaterialRequest.CostPrice,
                DateOfAssignment = historyDefectiveRavMaterialRequest.DateOfAssignment,
                Note = historyDefectiveRavMaterialRequest.Note,
                RawMaterialType = await new RawMaterialTypeProvider()
                    .GetRawMaterialTypeByNameAsync(historyDefectiveRavMaterialRequest.RawMaterialType)
                    .ConfigureAwait(false),
            };
        }
    }
}