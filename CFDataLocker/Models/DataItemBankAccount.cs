using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDataLocker.Models
{
    /// <summary>
    /// Bank account data item
    /// </summary>
    public class DataItemBankAccount : DataItemBase, ICloneable
    {
        public string AccountName { get; set; } = String.Empty;

        public string AccountNumber { get; set; } = String.Empty;

        public string SortCode { get; set; } = String.Empty;

        public override object Clone()
        {
            return new DataItemBankAccount()
            {
                Id = Id,
                AccountName = AccountName,
                AccountNumber = AccountNumber,
                Name = Name,
                Notes = Notes,
                SortCode = SortCode
            };
        }
    }
}
