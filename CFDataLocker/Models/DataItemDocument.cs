namespace CFDataLocker.Models
{
    /// <summary>
    /// Document data item. E.g. Photo of driving licence.
    /// </summary>
    public class DataItemDocument : DataItemBase
    {
        /// <summary>
        /// Path to document
        /// </summary>
        public string FilePath { get; set; } = String.Empty;

        public override object Clone()
        {
            return new DataItemDocument()
            {
                Id = Id,      
                FilePath = FilePath,
                Name = Name,
                Notes = Notes,                
            };
        }
    }
}
