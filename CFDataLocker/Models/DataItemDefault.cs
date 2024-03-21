namespace CFDataLocker.Models
{
    /// <summary>
    /// Default data item
    /// </summary>        
    public class DataItemDefault : DataItemBase, ICloneable
    {
        ///// <summary>
        ///// Unique Id
        ///// </summary>
        //public string Id { get; set; } = String.Empty;

        ///// <summary>
        ///// Name of item
        ///// </summary>        
        //public string Name { get; set; } = String.Empty;

        /// <summary>
        /// Account credentials
        /// </summary>
        public AccountCredentials Credentials { get; set; } = new AccountCredentials();

        /// <summary>
        /// Contact information
        /// </summary>
        public Contact Contact { get; set; } = new Contact();

        /// <summary>
        /// URL. E.g. Company website
        /// </summary>
        public string URL { get; set; } = String.Empty;

        /// <summary>
        /// Additional notes
        /// </summary>
        //public string Notes { get; set; } = String.Empty;       

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
