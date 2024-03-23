using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using CFDataLocker.Interfaces;
using CFDataLocker.Utilities;

namespace CFDataLocker.Services
{
    /// <summary>
    /// Secure items using secure storage
    /// </summary>
    public class SecureStorageSecureItemService : ISecureItemService
    {
        public string ReadItem(string name)
        {                        
            return SecureStorage.GetAsync(name).Result;            
        }

        public void WriteItem(string name, string value)
        {
            SecureStorage.SetAsync(name, value).Wait();
        }
    }
}
