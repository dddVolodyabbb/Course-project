using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryServer.Services.Crypt
{
    public interface ICryptService
    {
        public string ComputeHash(string text);
    }
}
