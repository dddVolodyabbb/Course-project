using System.Threading.Tasks;
using Inventory_server;
using Inventory_server.Commands.User;

namespace InventoryServer
{
    internal class Program
    {
        private const string ServerUri = "http://127.0.0.1:8756/";

        private static async Task Main(string[] args)
        {
            await CreateServer().StartAsync(ServerUri).ConfigureAwait(false);
        }
        private static IServer CreateServer()
        {
            return Locator.Current.Locate<IServer>();
        }
    }
}
