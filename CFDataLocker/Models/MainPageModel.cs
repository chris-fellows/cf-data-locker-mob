using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CFDataLocker.Interfaces;
using Kotlin.Reflect;

namespace CFDataLocker.Models
{
    /// <summary>
    /// View model for main page listing data items
    /// </summary>
    public class MainPageModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public LocalizationResources LocalizationResources => LocalizationResources.Instance;

        private readonly IDataLockerService _dataLockerService;

        private DataLocker _dataLocker;

        private ObservableCollection<DataItemBase> _dataItems = new ObservableCollection<DataItemBase>();        

        public void OnPropertyChanged([CallerMemberName] string name = "") =>
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private List<DataItemType> _dataItemTypes;

        public MainPageModel(IDataLockerService dataLockerService)
        {
            //dataLockerService.Delete("");

            // Set data item types
            _dataItemTypes = new List<DataItemType>()
            {
                new DataItemType() { Name = "Default" },
                new DataItemType() { Name = "Bank Account" },
                new DataItemType() { Name = "Credit Card" },
                new DataItemType() { Name = "Document" }
            };
            SelectedDataItemType = _dataItemTypes.First();

            _dataLockerService = dataLockerService;

            // Get data locker for user, create if not exists
            _dataLocker = _dataLockerService.GetByUserName(Environment.UserName);            

            //// Delete (For testing)
            //if (_dataLocker != null)
            //{
            //    _dataLockerService.Delete(_dataLocker.Id);
            //    _dataLocker = null;
            //}

            if (_dataLocker == null)   // Create data locker
            {
                _dataLocker = new DataLocker()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = Environment.UserName,                    
                    DataItems = new()
                    {
                        new DataItemDefault()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "Default",
                            Contact = new(),
                            Credentials = new(),
                            URL = "https://www.google.co.uk"
                        },
                        new DataItemBankAccount()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "My Bank Account",
                            AccountName = "Mr J Smith",
                            AccountNumber = "12345",
                            SortCode = "1234"                            
                        },
                        new DataItemCreditCard()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "My Credit Card",
                            CardType = "American Express",
                            CardNumber = "123456",
                            SecurityCode = "123",
                            ExpiryDate = "12/01/30",
                            Pin = "1234"
                        },
                        new DataItemDocument()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "Driving licence",                            
                        }
                    }
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

        public DataItemBase AddDataItem(string name, Type dataItemType)
        {
            DataItemBase dataItem = null;

            if (dataItemType is DataItemDefault)
            {
                dataItem = new DataItemDefault()
                {
                    Id = Guid.NewGuid().ToString(),
                    Contact = new Contact(),
                    Credentials = new AccountCredentials(),
                    Name = name
                };
            }
            else if (dataItemType is DataItemBankAccount)
            {
                dataItem = new DataItemBankAccount()
                {
                    Id = Guid.NewGuid().ToString(),                    
                    Name = name
                };
            }
            else if (dataItemType is DataItemCreditCard)
            {
                dataItem = new DataItemCreditCard()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = name
                };
            }
            else if (dataItemType is DataItemDocument)
            {
                dataItem = new DataItemDocument()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = name
                };
            }

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
            DataItemBase? selectedDataItem = String.IsNullOrEmpty(selectedDataItemId) ? 
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
    }
}
