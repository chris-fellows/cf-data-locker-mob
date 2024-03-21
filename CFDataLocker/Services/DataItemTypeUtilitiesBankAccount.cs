using CFDataLocker.Constants;
using CFDataLocker.Interfaces;
using CFDataLocker.Models;

namespace CFDataLocker.Services
{
    /// <summary>
    /// Data item type utilities for bank account
    /// </summary>
    public class DataItemTypeUtilitiesBankAccount : IDataItemTypeUtilities
    {
        public string InternalName => DataItemTypeInternalNames.BankAccount;

        public DataItemBase CreateNewDataItem(string name)
        {
            return new DataItemBankAccount()
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

            Shell.Current.GoToAsync(nameof(EditBankAccountPage), parameters);
        }
    }
}
