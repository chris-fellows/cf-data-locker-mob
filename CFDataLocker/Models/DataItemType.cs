namespace CFDataLocker.Models
{
    /// <summary>
    /// Data item type
    /// </summary>
    public class DataItemType
    {
        public string Name { get; set; } = String.Empty;           

        /// <summary>
        /// Instance type. Type derived from DataItemBase.
        /// </summary>
        public Type InstanceType { get; set; }
    }
}
