namespace CFDataLocker.Interfaces
{
    /// <summary>
    /// Interface for encryption
    /// </summary>
    public interface IEncryptionService
    {
        string EncryptToBase64String(string input);

        string DecryptFromBase64String(string input);

        byte[] EncryptToByteArray(byte[] input);

        byte[] DecryptFromByteArray(byte[] input);
    }
}
