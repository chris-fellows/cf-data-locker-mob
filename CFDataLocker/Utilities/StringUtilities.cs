using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDataLocker.Utilities
{
    /// <summary>
    /// String utilities
    /// </summary>
    internal class StringUtilities
    { 
        /// <summary>
        /// Encodes string to base 64
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string EncodeToBase64(string text)
        {
            var textBytes = System.Text.Encoding.UTF8.GetBytes(text);
            return System.Convert.ToBase64String(textBytes);
        }

        /// <summary>
        /// Decodes string from base 64
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static string DecodeFromBase64(string base64)
        {
            var base64Bytes = System.Convert.FromBase64String(base64);
            return System.Text.Encoding.UTF8.GetString(base64Bytes);
        }
    }
}
