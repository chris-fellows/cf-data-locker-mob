using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CFDataLocker.Utilities;

namespace CFDataLocker.Models
{
    /// <summary>
    /// Account credentials (E.g. Log in to website)
    /// </summary>
    public class AccountCredentials : ICloneable
    {
        public string Reference { get; set; } = String.Empty;            

        public string UserName { get; set; } = String.Empty;

        public string Password { get; set; } = String.Empty;

        public object Clone()
        {
            return new AccountCredentials()
            {
                Reference = Reference,
                UserName = UserName,
                Password = Password
            };
        }       
    }
}
