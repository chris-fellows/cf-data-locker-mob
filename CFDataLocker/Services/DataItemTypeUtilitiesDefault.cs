﻿using CFDataLocker.Constants;
using CFDataLocker.Interfaces;
using CFDataLocker.Models;

namespace CFDataLocker.Services
{
    /// <summary>
    /// Data item type utilities for default
    /// </summary>
    public class DataItemTypeUtilitiesDefault : IDataItemTypeUtilities
    {
        public string InternalName => DataItemTypeInternalNames.Default;

        public DataItemBase CreateNewDataItem(string name)
        {
            return new DataItemDefault()
            {
                Id = Guid.NewGuid().ToString(),
                Contact = new CFDataLocker.Models.Contact(),
                Credentials = new AccountCredentials(),
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

            Shell.Current.GoToAsync(nameof(EditDataItemPage), parameters);
        }
    }
}
