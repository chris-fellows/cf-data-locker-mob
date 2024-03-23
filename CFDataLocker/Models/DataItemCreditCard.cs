namespace CFDataLocker.Models
{
    /// <summary>
    /// Credit card data item
    /// </summary>
    public class DataItemCreditCard : DataItemBase, ICloneable
    {
        public string CardNumber { get; set; } = String.Empty;

        public string SecurityCode { get; set; } = String.Empty;

        public string ExpiryDate { get; set; } = String.Empty;

        public string Pin { get; set; } = String.Empty;

        public string CardType { get; set; } = String.Empty;

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
