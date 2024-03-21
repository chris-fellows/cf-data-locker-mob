using CFDataLocker.Models;

namespace CFDataLocker.Interfaces
{
    /// <summary>
    /// Interface for data item types
    /// </summary>
    public interface IDataItemTypeService
    {
        /// <summary>
        /// Gets all data item types
        /// </summary>
        /// <returns></returns>
        List<DataItemType> GetAll();

        /// <summary>
        /// Creates a new data item instance with basic properties set
        /// </summary>
        /// <param name="name"></param>
        /// <param name="instanceType"></param>
        /// <returns></returns>
        DataItemBase CreateNewDataItem(string name, Type instanceType);

        /// <summary>
        /// Navigates to page for edit of data item
        /// </summary>
        /// <param name="dataLockerId"></param>
        /// <param name="dataItem"></param>
        void NavigateEditPage(string dataLockerId, DataItemBase dataItem);

        /// <summary>
        /// Gets initial set of data items when app is first used. User can delete unused ones.
        /// </summary>
        /// <returns></returns>
        List<DataItemBase> GetInitialDataItems();
    }
}
