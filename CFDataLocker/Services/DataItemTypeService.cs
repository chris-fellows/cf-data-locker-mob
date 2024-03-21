using CFDataLocker.Interfaces;
using CFDataLocker.Models;

namespace CFDataLocker.Services
{
    public class DataItemTypeService : IDataItemTypeService
    {
        public List<DataItemType> GetAll()
        {           
            var dataItemTypes = new List<DataItemType>()
            {
                new DataItemType() { Name = LocalizationResources.Instance["DataItemTypeDefault"].ToString(),
                            InstanceType = typeof(DataItemDefault) },
                new DataItemType() { Name = LocalizationResources.Instance["DataItemTypeBankAccount"].ToString(),
                            InstanceType = typeof(DataItemBankAccount) },
                new DataItemType() { Name = LocalizationResources.Instance["DataItemTypeCreditCard"].ToString(),
                            InstanceType = typeof(DataItemCreditCard) },
                new DataItemType() { Name = LocalizationResources.Instance["DataItemTypeDocument"].ToString(),
                            InstanceType = typeof(DataItemDocument) }
            };
            return dataItemTypes;
        }
        
        public DataItemBase CreateNewDataItem(string name, Type dataItemType)
        {            
            if (dataItemType == typeof(DataItemDefault))
            {
                return new DataItemDefault()
                {
                    Id = Guid.NewGuid().ToString(),
                    Contact = new CFDataLocker.Models.Contact(),
                    Credentials = new AccountCredentials(),
                    Name = name
                };
            }
            else if (dataItemType == typeof(DataItemBankAccount))
            {
                return new DataItemBankAccount()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = name
                };
            }
            else if (dataItemType == typeof(DataItemCreditCard))
            {
                return new DataItemCreditCard()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = name
                };
            }
            else if (dataItemType == typeof(DataItemDocument))
            {
                return new DataItemDocument()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = name
                };
            }

            throw new ApplicationException($"Cannot create data item {dataItemType.Name}");
        }

        public void NavigateEditPage(string dataLockerId, DataItemBase dataItem)
        {
            var parameters = new Dictionary<string, object>
                {
                    { "LockerId", dataLockerId },
                    { "ItemId", dataItem.Id }
                };

            //Shell.Current.GoToAsync($"{nameof(EditDataItemPage)}?ItemId={dataItem.Id}");            
            if (dataItem is DataItemDefault)
            {
                Shell.Current.GoToAsync(nameof(EditDataItemPage), parameters);
            }
            else if (dataItem is DataItemBankAccount)
            {
                Shell.Current.GoToAsync(nameof(EditBankAccountPage), parameters);
            }
            else if (dataItem is DataItemCreditCard)
            {
                Shell.Current.GoToAsync(nameof(EditCreditCardPage), parameters);
            }
            else if (dataItem is DataItemDocument)
            {
                Shell.Current.GoToAsync(nameof(EditDocumentPage), parameters);
            }
        }

        public List<DataItemBase> GetInitialDataItems()
        {
            var dataItems = new List<DataItemBase>()
                    {
                        new DataItemDefault()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "Default",
                            Contact = new(),
                            Credentials = new(),
                            URL = "https://www.google.co.uk"
                        },
                        new DataItemDefault()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "Email Account",
                            Contact = new(),
                            Credentials = new()
                            {
                                UserName = "myemail@outlook.com",
                                Password = "123"
                            },
                            URL = "https://www.microsoft.com"
                        },
                        new DataItemDefault()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "NI number",
                            Contact = new(),
                            Credentials = new()
                        },
                        new DataItemDefault()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "NHS number",
                            Contact = new(),
                            Credentials = new()
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
                        },
                         new DataItemDocument()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "Birth certificate",
                        }
                    };

            return dataItems;
        }
    }
}
