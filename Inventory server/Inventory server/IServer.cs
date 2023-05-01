using System;
using System.Threading.Tasks;

namespace NP_08_Server
{
    public interface IServer : IDisposable
    {
        public Task StartAsync(string uri);
    }
}