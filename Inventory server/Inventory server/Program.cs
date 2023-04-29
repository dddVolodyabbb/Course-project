using NP_08_Server;
using System.Threading.Tasks;

namespace Inventory_server
{
    internal class Program
    {
        private const string ServerUri = "http://127.0.0.1:8756/";
        static async Task Main(string[] args)
        {
            await CreateServer().StartAsync(ServerUri).ConfigureAwait(false);
        }
        private static IServer CreateServer()
        {
            return new Server(

                );
        }
    }
}
