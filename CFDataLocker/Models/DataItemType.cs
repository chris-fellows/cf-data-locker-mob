using CFDataLocker.Enums;

namespace CFDataLocker.Models
{
    /// <summary>
    /// Data item type
    /// </summary>
    public class DataItemType
    {        
        public DataItemTypes ItemType { get; set; } = DataItemTypes.Default;

        public string Name { get; set; } = String.Empty;
        
        public Type ModelInstanceType { get; set; } = typeof(System.Object);
    }
}
