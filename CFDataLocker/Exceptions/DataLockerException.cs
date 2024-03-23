using System.Globalization;

namespace CFDataLocker.Exceptions
{
    /// <summary>
    /// General data locker exception.
    /// </summary>
    /// <remarks>Use this when there is no more specific exception</remarks>
    public class DataLockerException : Exception
    {
        public DataLockerException()
        {
        }

        public DataLockerException(string message) : base(message)
        {
        }

        public DataLockerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public DataLockerException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
