using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using InventoryServer.Context.Contexts;

namespace InventoryServer.Commands
{
    public interface ICommand
    {
        public string Path { get; }
        public HttpMethod Method { get; }
        public Task HandleRequestAsync(HttpListenerContext context);
    }
}