using CFDataLocker.Constants;
using CFDataLocker.Interfaces;
using CFDataLocker.Models;

namespace CFDataLocker.Services
{
    /// <summary>
    /// Data item type utilities for credit card
    /// </summary>
    public class DataItemTypeUtilitiesCreditCard : IDataItemTypeUtilities
    {
        public string InternalName => DataItemTypeInternalNames.CreditCard;

        public DataItemBase CreateNewDataItem(string name)
        {
            return new DataItemCreditCard()
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

            Shell.Current.GoToAsync(nameof(EditCreditCardPage), parameters);
        }
    }
}
