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
        /// Gets utilities from data type internal name
        /// </summary>
        /// <param name="internalName"></param>
        /// <returns></returns>
        IDataItemTypeUtilities GetUtilities(string internalName);        

        /// <summary>
        /// Gets utilities from data type instance type
        /// </summary>
        /// <param name="dataItemInstanceType"></param>
        /// <returns></returns>
        IDataItemTypeUtilities GetUtilities(Type dataItemInstanceType);     

        /// <summary>
        /// Gets initial set of data items when app is first used. User can delete unused ones.
        /// </summary>
        /// <returns></returns>
        List<DataItemBase> GetInitialDataItems();
    }
}
