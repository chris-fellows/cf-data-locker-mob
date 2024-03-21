using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CFDataLocker.Utilities;

namespace CFDataLocker.Models
{
    /// <summary>
    /// Contact details
    /// </summary>
    public class Contact : ICloneable
    {
        public string Email { get; set; } = String.Empty;            

        public string PhoneNumber { get; set; } = String.Empty;

        public object Clone()
        {
            return new Contact()
            {
                Email = Email,
                PhoneNumber = PhoneNumber
            };
        }
    }
}
