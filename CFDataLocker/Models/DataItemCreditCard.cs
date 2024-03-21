using Android.Net;
using Java.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDataLocker.Models
{
    /// <summary>
    /// Credit card data item
    /// </summary>
    public class DataItemCreditCard : DataItemBase, ICloneable
    {
        public string CardNumber { get; set; }

        public string SecurityCode { get; set;  }

        public string ExpiryDate { get; set; }

        public string Pin { get; set; }

        public string CardType { get; set; }

        public override object Clone()
        {
            return new DataItemCreditCard()
            {
                Id = Id,
                Name = Name,
                CardNumber = CardNumber,
                CardType = CardType,
                ExpiryDate = ExpiryDate, 
                SecurityCode= SecurityCode,                
                Pin = Pin,
                Notes = Notes
            };
        }
    }
}
