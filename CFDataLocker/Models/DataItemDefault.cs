namespace CFDataLocker.Models
{
    /// <summary>
    /// Default data item
    /// </summary>        
    public class DataItemDefault : DataItemBase, ICloneable
    {       
        /// <summary>
        /// Account credentials
        /// </summary>
        public AccountCredentials Credentials { get; set; } = new AccountCredentials();

        /// <summary>
        /// Contact information. E.g. Contact information for utilities provider
        /// </summary>
        public Contact Contact { get; set; } = new Contact();

        /// <summary>
        /// URL. E.g. Website to log in to
        /// </summary>
        public string URL { get; set; } = String.Empty;  

        public override object Clone()
        {
            return new DataItemDefault()
            {
                Id = Id,
                Name = Name,
                Credentials = Credentials == null ? null : (AccountCredentials)Credentials.Clone(),
                Contact = Contact == null ? null : (Contact)Contact.Clone(),
                URL = URL,
                Notes = Notes
            };            
        }
    }
}
