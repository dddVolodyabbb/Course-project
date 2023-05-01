using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using InventoryServer.Context.Contexts;

namespace NP_08_Server
{
    public interface ICommand
    {
        public ContextInventory DbContextInventory { get; }
        public string Path { get; }
        public HttpMethod Method { get; }
        public Task HandleRequestAsync(HttpListenerContext context);
    }
}