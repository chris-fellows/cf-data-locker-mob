﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Android.Media;
using CFDataLocker.Interfaces;
using CFDataLocker.Models;

namespace CFDataLocker.ViewModels
{
    //public class EditDataItemPageModel : EditDataItemModelBase<DataItemDefault>
    //{
    //    public EditDataItemPageModel(IDataLockerService dataLockerService) : base(dataLockerService) { }    
    //}

    /// <summary>
    /// Model for edit of default data item
    /// </summary>
    public class EditDefaultPageModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public LocalizationResources LocalizationResources => LocalizationResources.Instance;

        private DataLocker _dataLocker = new DataLocker();
        private DataItemDefault _dataItem = new DataItemDefault();
        private readonly IDataLockerService _dataLockerService;

        public void OnPropertyChanged([CallerMemberName] string name = "") =>
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public EditDefaultPageModel(IDataLockerService dataLockerService)
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
                _dataItem = (DataItemDefault)_dataLocker.DataItems.First(di => di.Id == _dataItemId);

                OnPropertyChanged(nameof(SelectedDataItem));
            }
        }

        public DataItemDefault SelectedDataItem
        {
            get { return _dataItem; }
        }

        public void SaveChanges()
        {
            _dataLockerService.Update(_dataLocker);
        }
    }
}
