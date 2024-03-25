using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
//using Android.OS;
using CFDataLocker.Interfaces;
using CFDataLocker.Models;
using Plugin.Fingerprint.Abstractions;

namespace CFDataLocker.ViewModels
{
    /// <summary>
    /// View model for main page listing data items
    /// </summary>
    public class MainPageModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public LocalizationResources LocalizationResources => LocalizationResources.Instance;

        private readonly IDataItemTypeService _dataItemTypeService;
        private readonly IDataLockerService _dataLockerService;

        private DataLocker _dataLocker;

        private ObservableCollection<DataItemBase> _dataItems = new ObservableCollection<DataItemBase>();

        public void OnPropertyChanged([CallerMemberName] string name = "") =>
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private List<DataItemType> _dataItemTypes;

        private readonly IFingerprint _fingerprint;

        public bool IsNeedFingerPrint => true;      // Default

        public MainPageModel(IDataItemTypeService dataItemTypeService,
                            IDataLockerService dataLockerService,
                            IFingerprint fingerprint)
        {
            _dataItemTypeService = dataItemTypeService;            

            // Set data item types
            _dataItemTypes = _dataItemTypeService.GetAll();
            SelectedDataItemType = _dataItemTypes.First();

            _dataLockerService = dataLockerService;

            _fingerprint = fingerprint;

            // Get data locker for user, create if not exists
            _dataLocker = _dataLockerService.GetByUserName(Environment.UserName);

            if (_dataLocker == null)   // Create data locker
            {
                _dataLocker = new DataLocker()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = Environment.UserName,
                    DataItems = _dataItemTypeService.GetInitialDataItems()
                };
                _dataLockerService.Update(_dataLocker);
            }

            //var test = _dataLockerService.GetById(_dataLocker.Id);

            _dataItems = new ObservableCollection<DataItemBase>(_dataLocker.DataItems);
        }

        public List<DataItemType> DataItemTypes => _dataItemTypes;

        public DataItemType SelectedDataItemType { get; set; }

        public string? DataLockerId => _dataLocker?.Id;

        public ObservableCollection<DataItemBase> DataItems => _dataItems;

        public void EditDataItem(DataItemBase dataItem)
        {
            var utilities = _dataItemTypeService.GetUtilities(dataItem.GetType());
            utilities.NavigateEditPage(_dataLocker.Id, dataItem);

            //_dataItemTypeService.NavigateEditPage(_dataLocker.Id, dataItem);
        }

        public DataItemBase AddDataItem(string name, Type dataItemType)
        {
            //DataItemBase dataItem = _dataItemTypeService.CreateNewDataItem(name, dataItemType);
            var utilities = _dataItemTypeService.GetUtilities(dataItemType);
            var dataItem = utilities.CreateNewDataItem(name);

            _dataLocker.DataItems.Add(dataItem);
            _dataLocker.DataItems = _dataLocker.DataItems.OrderBy(di => di.Name).ToList();   // Alphabetic order
            _dataLockerService.Update(_dataLocker);

            RefreshDataItems(dataItem.Id);

            return dataItem;
        }

        public void DeleteDataItem(string dataItemId)
        {
            _dataLocker.DataItems.RemoveAll(di => di.Id == dataItemId);
            _dataLockerService.Update(_dataLocker);

            // Refresh, don't select other data item
            RefreshDataItems(null);
        }

        /// <summary>
        /// Refreshes data items. Also sets/clears selected data item
        /// </summary>
        public void RefreshDataItems(string? selectedDataItemId)
        {
            // Refresh
            _dataItems.Clear();
            _dataLocker = _dataLockerService.GetById(_dataLocker.Id);
            _dataItems = new ObservableCollection<DataItemBase>(_dataLocker.DataItems);

            OnPropertyChanged(nameof(DataItems));
            OnPropertyChanged(nameof(IsDataItemSelected));

            // Set selected data item or unset
            DataItemBase? selectedDataItem = string.IsNullOrEmpty(selectedDataItemId) ?
                    null :
                    _dataLocker.DataItems.FirstOrDefault(di => di.Id == selectedDataItemId);
            SelectedDataItem = selectedDataItem;
        }

        public bool IsDataItemSelected => _selectedDataItem != null;

        private DataItemBase? _selectedDataItem;
        public DataItemBase? SelectedDataItem
        {
            get { return _selectedDataItem; }
            set
            {
                _selectedDataItem = value;

                OnPropertyChanged(nameof(SelectedDataItem));
                OnPropertyChanged(nameof(IsDataItemSelected));
            }
        }

        /// <summary>
        /// Checks user fingerprint
        /// </summary>
        /// <returns>Whether authenticated</returns>
        public async Task<bool> CheckFingerprint()
        {            
            if (IsNeedFingerPrint)
            {
                /*
                var dialogConfig = new AuthenticationRequestConfiguration
                      ("Login using biometrics", "Confirm login with your biometrics")
                        {
                            FallbackTitle = "Use Password",
                            AllowAlternativeAuthentication = true,
                        };

                        var resultXX = await _fingerprint.AuthenticateAsync(dialogConfig);
                */                
               
                var isAuthenticated = false;
                var isAvailable = await _fingerprint.IsAvailableAsync();                
                if (isAvailable)
                {
                    var request = new AuthenticationRequestConfiguration(LocalizationResources.Instance["FingerprintTitle"].ToString(),
                               LocalizationResources.Instance["FingerprintReasonEditDataItem"].ToString());

                    var result = await _fingerprint.AuthenticateAsync(request);

                    isAuthenticated = result.Authenticated;
                }

                return isAuthenticated;
            }
            else
            {
                return true;
            }
        }
    }
}
