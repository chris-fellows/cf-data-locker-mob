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

        public byte[] GetKey(string type, int byteLength)
        {
            var key = new byte[0];
            var itemKey = $"DataLocker.{type}.{Environment.UserName}";
            var itemValue = ReadItem(itemKey);
            if (String.IsNullOrEmpty(itemValue))    // New random key
            {
                key = AesEncryptionUtilities.CreateRandomKeyOrIV(byteLength);
                WriteItem(itemKey, Convert.ToBase64String(key));
            }
            else   // Existing key
            {
                key = Convert.FromBase64String(itemValue);
            }
            return key;
        }
    }
}
