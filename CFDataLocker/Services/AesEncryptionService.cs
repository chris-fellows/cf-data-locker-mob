using CFDataLocker.Interfaces;
using CFDataLocker.Utilities;

namespace CFDataLocker.Services
{
    /// <summary>
    /// Encryption via AES. The corresponding Encrypt and Decrypt methods must be used.
    /// </summary>
    public class AesEncryptionService : IEncryptionService
    {
        private ISecureItemService _secureItemService;

        private const string _ivKeyName = "IV";
        private const string _keyKeyName = "Key";
        private const int _ivKeyLength = 16;    // 128-bit IV
        private const int _keyKeyLength = 32;   // 256-bit key

        public AesEncryptionService(ISecureItemService secureItemService)
        {
            _secureItemService = secureItemService;
        }

        public string EncryptToBase64String(string input)
        {
            var iv = GetSecureKey(_ivKeyName, _ivKeyLength);
            var key = GetSecureKey(_keyKeyName, _keyKeyLength);
            return Convert.ToBase64String(AesEncryptionUtilities.Encrypt(input, key, iv));
        }

        public string DecryptFromBase64String(string input)
        {
            var iv = GetSecureKey(_ivKeyName, _ivKeyLength);
            var key = GetSecureKey(_keyKeyName, _keyKeyLength);
            return AesEncryptionUtilities.Decrypt(Convert.FromBase64String(input), key, iv);
        }

        public byte[] EncryptToByteArray(byte[] input)
        {
            var iv = GetSecureKey(_ivKeyName, _ivKeyLength);
            var key = GetSecureKey(_keyKeyName, _keyKeyLength);
            return AesEncryptionUtilities.Encrypt(Convert.ToBase64String(input), key, iv);           
        }

        public byte[] DecryptFromByteArray(byte[] input)
        {
            var iv = GetSecureKey(_ivKeyName, _ivKeyLength);
            var key = GetSecureKey(_keyKeyName, _keyKeyLength);
            return Convert.FromBase64String(AesEncryptionUtilities.Decrypt(input, key, iv));
        }

        /// <summary>
        /// Gets secure key or creates a new random key if not exists
        /// </summary>        
        /// <param name="type">Type (Key/IV etc)</param>
        /// <param name="byteLength">Key length (Bytes)</param>
        /// <returns>Key</returns>
        private byte[] GetSecureKey(string type, int byteLength)
        {
            var key = new byte[0];
            var itemKey = $"DataLocker.{type}.{Environment.UserName}";
            var itemValue = _secureItemService.ReadItem(itemKey);
            if (String.IsNullOrEmpty(itemValue))    // New random key
            {
                key = AesEncryptionUtilities.CreateRandomKeyOrIV(byteLength);
                _secureItemService.WriteItem(itemKey, Convert.ToBase64String(key));
            }
            else   // Existing key
            {
                key = Convert.FromBase64String(itemValue);
            }
            return key;
        }
    }
}
