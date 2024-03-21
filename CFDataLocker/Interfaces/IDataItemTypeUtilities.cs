using CFDataLocker.Models;

namespace CFDataLocker.Interfaces
{
    /// <summary>
    /// Utilities for specific data item type
    /// </summary>
    public interface IDataItemTypeUtilities
    {
        /// <summary>
        /// Data type internal name
        /// </summary>
        string InternalName { get; }

        /// <summary>
        /// Creates new data item with basic properties set
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        DataItemBase CreateNewDataItem(string name);

        /// <summary>
        /// Navigates to edit page for data item
        /// </summary>
        /// <param name="dataLockerId"></param>
        /// <param name="dataItem"></param>
        void NavigateEditPage(string dataLockerId, DataItemBase dataItem);
    }
}
