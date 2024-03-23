using CFDataLocker.Enums;
using CFDataLocker.Interfaces;
using CFDataLocker.Models;

namespace CFDataLocker.Services
{
    /// <summary>
    /// Data item type utilities for document
    /// </summary>
    public class DataItemTypeUtilitiesDocument : IDataItemTypeUtilities
    {
        public DataItemTypes DataItemType => DataItemTypes.Document;

        public Type ModelInstanceType => typeof(DataItemDocument);

        public string NameResourceName => "DataItemTypeDocument";

        public DataItemBase CreateNewDataItem(string name)
        {
            return new DataItemDocument()
            {
                Id = Guid.NewGuid().ToString(),
                Name = name
            };
        }

        public void NavigateEditPage(string dataLockerId, DataItemBase dataItem)
        {
            var parameters = new Dictionary<string, object>
                {
                    { "LockerId", dataLockerId },
                    { "ItemId", dataItem.Id }
                };

            Shell.Current.GoToAsync(nameof(EditDocumentPage), parameters);
        }
    }
}
