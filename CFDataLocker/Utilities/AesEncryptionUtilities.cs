using System.Security.Cryptography;
using System.Text;

namespace CFDataLocker.Utilities
{
    /// <summary>
    /// AES encryption utilities
    /// </summary>
    internal class AesEncryptionUtilities
    {
        /// <summary>
        /// Encrypts string
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="key">Key</param>
        /// <param name="iv">Initialization vector</param>
        /// <returns></returns>
        public static byte[] Encrypt(string plainText, byte[] key, byte[] iv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                byte[] encryptedBytes;
                using (var msEncrypt = new System.IO.MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                        csEncrypt.Write(plainBytes, 0, plainBytes.Length);
                    }
                    encryptedBytes = msEncrypt.ToArray();
                }
                return encryptedBytes;
            }
        }

        /// <summary>
        /// Decrypts bytes to string
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="key">Key</param>
        /// <param name="iv">Initialization vector</param>
        /// <returns></returns>
        public static string Decrypt(byte[] cipherText, byte[] key, byte[] iv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                byte[] decryptedBytes;
                using (var msDecrypt = new System.IO.MemoryStream(cipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var msPlain = new System.IO.MemoryStream())
                        {
                            csDecrypt.CopyTo(msPlain);
                            decryptedBytes = msPlain.ToArray();
                        }
                    }
                }
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }

        /// <summary>
        /// Creates random key or IV of specified byte length
        /// </summary>
        /// <param name="bytesLength"></param>
        /// <returns>Key or IV</returns>
        public static byte[] CreateRandomKeyOrIV(int bytesLength)
        {
            var bytes = new byte[bytesLength];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);                
            }
            return bytes;
        }

        public static void Test()
        {
            string plaintext = "Hello, World!";
            Console.WriteLine(plaintext);
            // Generate a random key and IV
            byte[] key = new byte[32]; // 256-bit key
            byte[] iv = new byte[16]; // 128-bit IV
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(key);
                rng.GetBytes(iv);
            }

            // Encrypt
            byte[] ciphertext = Encrypt(plaintext, key, iv);
            string encryptedText = Convert.ToBase64String(ciphertext);
            Console.WriteLine("Encrypted Text: " + encryptedText);
            // Decrypt
            byte[] bytes = Convert.FromBase64String(encryptedText);
            string decryptedText = Decrypt(bytes, key, iv);
            Console.WriteLine("Decrypted Text: " + decryptedText);

            int xxxx = 1000;
        }
    }
}
