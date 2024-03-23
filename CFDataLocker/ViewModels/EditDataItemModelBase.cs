using System;
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
    public abstract class EditDataItemModelBase<TItemType> where TItemType : DataItemBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public LocalizationResources LocalizationResources => LocalizationResources.Instance;

        protected DataLocker _dataLocker = new DataLocker();
        protected TItemType _dataItem = default;
        protected readonly IDataLockerService _dataLockerService;

        public void OnPropertyChanged([CallerMemberName] string name = "") =>
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public EditDataItemModelBase(IDataLockerService dataLockerService)
        {
            _dataLockerService = dataLockerService;
        }

        protected string _dataLockerId;
        public string DataLockerId
        {
            set
            {
                _dataLockerId = value;
                OnQueryPropertySet();
            }
        }

        protected string _dataItemId;

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
        protected void OnQueryPropertySet()
        {
            if (!string.IsNullOrEmpty(_dataLockerId) &&
                !string.IsNullOrEmpty(_dataItemId))     // Load data locker & item
            {
                _dataLocker = _dataLockerService.GetById(_dataLockerId);
                _dataItem = (TItemType)_dataLocker.DataItems.First(di => di.Id == _dataItemId);

                OnPropertyChanged(nameof(SelectedDataItem));
            }
        }

        public TItemType SelectedDataItem
        {
            get { return _dataItem; }
        }

        public void SaveChanges()
        {
            _dataLockerService.Update(_dataLocker);
        }
    }
}
