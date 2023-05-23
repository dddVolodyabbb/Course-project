using System;
using System.Threading.Tasks;

namespace InventoryServer
{
    public interface IServer : IDisposable
    {
        public Task StartAsync(string uri);
    }
}