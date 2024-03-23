using CFDataLocker.Utilities;
using Xunit;

namespace CFDataLocker.Test
{
    /// <summary>
    /// AES encryption tests to test that the base algorithms work
    /// </summary>
    public class AesEncryptionTests
    {
        [Fact]
        public async Task Encrypt_String_Succeeds()
        {
            var key = AesEncryptionUtilities.CreateRandomKeyOrIV(32);
            var iv = AesEncryptionUtilities.CreateRandomKeyOrIV(16);
            var result = AesEncryptionUtilities.Encrypt("Hello world", key, iv);
            var x = 1000;
        }

        //[Fact]
        //public async Task Encrypt_And_Decrypt_String_Succeeds()
        //{
        //    var input = "Hello world";
        //    var key = AesEncryptionUtilities.CreateRandomKeyOrIV(32);
        //    var iv = AesEncryptionUtilities.CreateRandomKeyOrIV(16);
        //    var encrypted = AesEncryptionUtilities.Encrypt(input, key, iv);

        //    var dectypted = AesEncryptionUtilities.Decrypt()
        //    var x = 1000;
        //}
    }
}
