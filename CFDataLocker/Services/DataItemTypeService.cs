using CFDataLocker.Enums;
using CFDataLocker.Interfaces;
using CFDataLocker.Models;

namespace CFDataLocker.Services
{
    public class DataItemTypeService : IDataItemTypeService
    {
        private List<IDataItemTypeUtilities> _utilities = new();


        public DataItemTypeService(IEnumerable<IDataItemTypeUtilities> utilities)
        {
            _utilities = utilities.ToList();        
        }

        public List<DataItemType> GetAll()
        {
            return _utilities.Select(u => new DataItemType()
            {
                ItemType = u.DataItemType,
                Name = LocalizationResources.Instance[u.NameResourceName].ToString(),
                ModelInstanceType = u.ModelInstanceType
            }).OrderBy(i => i.Name).ToList();         
        }

        public IDataItemTypeUtilities GetUtilities(DataItemTypes dataItemType)
        {
            return _utilities.First(u => u.DataItemType == dataItemType);
        }

        public IDataItemTypeUtilities GetUtilities(Type dataItemInstanceType)
        {
            var dataItemType = GetAll().First(dit => dit.ModelInstanceType == dataItemInstanceType);
            return _utilities.First(u => u.DataItemType == dataItemType.ItemType);
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
