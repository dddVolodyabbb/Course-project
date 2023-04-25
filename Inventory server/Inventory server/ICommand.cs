using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace NP_08_Server;

public interface ICommand
{
	public string Path { get; }
	public HttpMethod Method { get; }
	public Task HandleRequestAsync(HttpListenerContext context);
}