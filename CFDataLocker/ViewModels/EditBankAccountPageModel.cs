﻿using CFDataLocker.Interfaces;
using CFDataLocker.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CFDataLocker.ViewModels
{
    /// <summary>
    /// Model for edit of bank account data item
    /// </summary>
    public class EditBankAccountPageModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public LocalizationResources LocalizationResources => LocalizationResources.Instance;

        private DataLocker _dataLocker = new DataLocker();
        private DataItemBankAccount _dataItem = new DataItemBankAccount();
        private readonly IDataLockerService _dataLockerService;

        public void OnPropertyChanged([CallerMemberName] string name = "") =>
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public EditBankAccountPageModel(IDataLockerService dataLockerService)
        {
            _dataLockerService = dataLockerService;
        }

        private string _dataLockerId;
        public string DataLockerId
        {
            set
            {
                _dataLockerId = value;
                OnQueryPropertySet();
            }
        }

        private string _dataItemId;

        public string DataItemId
        {
            set
            {
                _dataItemId = value;
                OnQueryPropertySet();
            }
        }

        /// <summary>
        /// Handles incoming query property set
        /// </summary>
        private void OnQueryPropertySet()
        {
            if (!string.IsNullOrEmpty(_dataLockerId) &&
                !string.IsNullOrEmpty(_dataItemId))     // Load data locker & item
            {
                _dataLocker = _dataLockerService.GetById(_dataLockerId);
                _dataItem = (DataItemBankAccount)_dataLocker.DataItems.First(di => di.Id == _dataItemId);

                OnPropertyChanged(nameof(SelectedDataItem));
            }
        }

        public DataItemBankAccount SelectedDataItem
        {
            get { return _dataItem; }
        }

        public void SaveChanges()
        {
            _dataLockerService.Update(_dataLocker);
        }
    }
}
