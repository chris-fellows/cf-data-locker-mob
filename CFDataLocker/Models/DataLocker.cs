namespace CFDataLocker.Models
{
    /// <summary>
    /// Locker for data items for particular user.
    /// </summary>
    public class DataLocker : ICloneable
    {
        /// <summary>
        /// Unique Id
        /// </summary>
        public string Id { get; set; } = String.Empty;

        /// <summary>
        /// User that owns this data locker
        /// </summary>
        public string UserName { get; set; } = String.Empty;

        /// <summary>
        /// Data items
        /// </summary>
        public List<DataItemBase> DataItems = new List<DataItemBase>();

        public object Clone()
        {
            return new DataLocker()
            {
                Id = Id,
                UserName = UserName,
                DataItems = DataItems.Select(dataItem => (DataItemBase)dataItem.Clone()).ToList()
            };
        }
    }
}
