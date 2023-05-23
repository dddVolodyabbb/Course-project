using System.Threading.Tasks;
using System;

namespace InventoryServer.Common.Extensions
{
    public static class TaskExtensions
    {
        public static async void FireAndForgetSafeAsync(this Task task)
        {
            try
            {
                await task.ConfigureAwait(false);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}