using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace InventoryServer.Commands
{
    public interface ICommand
    {
        public string Path { get; }
        public HttpMethod Method { get; }
        public Task HandleRequestAsync(HttpListenerContext context, Match path);
    }
}