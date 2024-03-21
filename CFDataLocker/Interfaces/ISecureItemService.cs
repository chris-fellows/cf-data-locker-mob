using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDataLocker.Interfaces
{
    /// <summary>
    /// Interface for storing string items securely
    /// </summary>
    public interface ISecureItemService
    {
        string ReadItem(string name);

        void WriteItem(string name, string value);
    }
}
