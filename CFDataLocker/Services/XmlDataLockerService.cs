using CFDataLocker.Interfaces;
using CFDataLocker.Models;
using CFDataLocker.Utilities;
using System.Text;

namespace CFDataLocker.Services
{
    /// <summary>
    /// Stores data locker instances in encrypted XML format
    /// </summary>
    public class XmlDataLockerService : IDataLockerService
    {
        private readonly string _folder;
        private readonly IEncryptionService _encryptionService;       

        public XmlDataLockerService(string folder, IEncryptionService encryptionService)                         
        {
            _folder = folder;
            _encryptionService = encryptionService;
        }

        // App only needs to use data locker for current user
        //public List<DataLocker> GetAll()
        //{
        //    var dataLockers = new List<DataLocker>();
        //    var files = Directory.GetFiles(_folder, "*.locker");
        //    foreach(var file in files)
        //    {
        //        var dataLocker = XmlUtilities.DeserializeFromString<DataLocker>(File.ReadAllText(file, Encoding.UTF8));
        //        Decrypt(dataLocker);
        //        dataLockers.Add(dataLocker);
        //    }
        //    return dataLockers;
        //}

        public DataLocker? GetById(string id)
        {
            var file = Path.Combine(_folder, $"{id}.locker");
            if (File.Exists(file))
            {
                var dataLocker = XmlUtilities.DeserializeFromString<DataLocker>(File.ReadAllText(file, Encoding.UTF8));
                Decrypt(dataLocker);
                return dataLocker;
            }
            return null;
        }

        public DataLocker? GetByUserName(string userName)
        {
            // User can only see their own data locker
            //if (userName != Environment.UserName) return null;

            var files = Directory.GetFiles(_folder, "*.locker");
            foreach (var file in files)
            {
                var dataLocker = XmlUtilities.DeserializeFromString<DataLocker>(File.ReadAllText(file, Encoding.UTF8));
                Decrypt(dataLocker);
                if (dataLocker.UserName == userName) return dataLocker;
            }
            return null;
        }

        public void Update(DataLocker dataLocker)
        {
            var dataLockerCopy = (DataLocker)dataLocker.Clone();
            Encrypt(dataLockerCopy);
            var file = Path.Combine(_folder, $"{dataLocker.Id}.locker");
            File.WriteAllText(file, XmlUtilities.SerializeToString(dataLockerCopy), Encoding.UTF8);
        }

        public void Delete(string id)
        {          
            var file = Path.Combine(_folder, $"{id}.locker");
            if (File.Exists(file)) File.Delete(file);
        }

        private void Encrypt(DataLocker dataLocker)
        {
            dataLocker.UserName = _encryptionService.EncryptToBase64String(dataLocker.UserName);
            foreach(var dataItem in dataLocker.DataItems)
            {
                if (dataItem is DataItemDefault)
                {
                    Encrypt((DataItemDefault)dataItem);
                }
                else if (dataItem is DataItemBankAccount)
                {
                    Encrypt((DataItemBankAccount)dataItem);
                }
                else if (dataItem is DataItemCreditCard)
                {
                    Encrypt((DataItemCreditCard)dataItem);
                }
                else if (dataItem is DataItemDocument)
                {
                    Encrypt((DataItemDocument)dataItem);
                }
            }        
        }

        private void Encrypt(DataItemDefault dataItem)
        {
            dataItem.Id = _encryptionService.EncryptToBase64String(dataItem.Id);
            dataItem.Name = _encryptionService.EncryptToBase64String(dataItem.Name);
            dataItem.Notes = _encryptionService.EncryptToBase64String(dataItem.Notes);
            dataItem.URL = _encryptionService.EncryptToBase64String(dataItem.URL);

            Encrypt(dataItem.Contact);           
            Encrypt(dataItem.Credentials);
        }

        private void Encrypt(DataItemBankAccount dataItem)
        {
            dataItem.Id = _encryptionService.EncryptToBase64String(dataItem.Id);
            dataItem.Name = _encryptionService.EncryptToBase64String(dataItem.Name);
            dataItem.Notes = _encryptionService.EncryptToBase64String(dataItem.Notes);
            dataItem.AccountName = _encryptionService.EncryptToBase64String(dataItem.AccountName);
            dataItem.AccountNumber = _encryptionService.EncryptToBase64String(dataItem.AccountNumber);
            dataItem.SortCode = _encryptionService.EncryptToBase64String(dataItem.SortCode);
        }

        private  void Encrypt(DataItemCreditCard dataItem)
        {
            dataItem.Id = _encryptionService.EncryptToBase64String(dataItem.Id);
            dataItem.Name = _encryptionService.EncryptToBase64String(dataItem.Name);
            dataItem.Notes = _encryptionService.EncryptToBase64String(dataItem.Notes); 
            dataItem.CardType = _encryptionService.EncryptToBase64String(dataItem.CardType);
            dataItem.CardNumber = _encryptionService.EncryptToBase64String(dataItem.CardNumber);
            dataItem.SecurityCode = _encryptionService.EncryptToBase64String(dataItem.SecurityCode);
            dataItem.ExpiryDate = _encryptionService.EncryptToBase64String(dataItem.ExpiryDate);
            dataItem.Pin = _encryptionService.EncryptToBase64String(dataItem.Pin);
        }

        private void Encrypt(AccountCredentials accountCredentials)
        {
            accountCredentials.Reference = _encryptionService.EncryptToBase64String(accountCredentials.Reference);
            accountCredentials.UserName = _encryptionService.EncryptToBase64String(accountCredentials.UserName);
            accountCredentials.Password = _encryptionService.EncryptToBase64String(accountCredentials.Password);
        }

        private void Encrypt(CFDataLocker.Models.Contact contact)
        {
            contact.Email = _encryptionService.EncryptToBase64String(contact.Email);
            contact.PhoneNumber = _encryptionService.EncryptToBase64String(contact.PhoneNumber);
        }

        private void Encrypt(DataItemDocument dataItem)
        {
            dataItem.Id = _encryptionService.EncryptToBase64String(dataItem.Id);
            dataItem.Name = _encryptionService.EncryptToBase64String(dataItem.Name);
            dataItem.Notes = _encryptionService.EncryptToBase64String(dataItem.Notes);            
            dataItem.FilePath = _encryptionService.EncryptToBase64String(dataItem.FilePath);
        }

        private void Decrypt(DataLocker dataLocker)
        {
            dataLocker.UserName = _encryptionService.DecryptFromBase64String(dataLocker.UserName);

            foreach(var dataItem in dataLocker.DataItems)
            {
                if (dataItem is DataItemDefault)
                {
                    Decrypt((DataItemDefault)dataItem);
                }
                else if (dataItem is DataItemBankAccount)
                {
                    Decrypt((DataItemBankAccount)dataItem);
                }
                else if (dataItem is DataItemCreditCard)
                {
                    Decrypt((DataItemCreditCard)dataItem);
                }
                else if (dataItem is DataItemDocument)
                {
                    Decrypt((DataItemDocument)dataItem);
                }
            }    
        }

        private void Decrypt(DataItemDefault dataItem)
        {
            dataItem.Id = _encryptionService.DecryptFromBase64String(dataItem.Id);
            dataItem.Name = _encryptionService.DecryptFromBase64String(dataItem.Name);
            dataItem.Notes = _encryptionService.DecryptFromBase64String(dataItem.Notes);
            dataItem.URL = _encryptionService.DecryptFromBase64String(dataItem.URL);

            Decrypt(dataItem.Contact);
            Decrypt(dataItem.Credentials);
        }

        private void Decrypt(DataItemBankAccount dataItem)
        {
            dataItem.Id = _encryptionService.DecryptFromBase64String(dataItem.Id);
            dataItem.Name = _encryptionService.DecryptFromBase64String(dataItem.Name);
            dataItem.Notes = _encryptionService.DecryptFromBase64String(dataItem.Notes);
            dataItem.AccountName = _encryptionService.DecryptFromBase64String(dataItem.AccountName);
            dataItem.AccountNumber = _encryptionService.DecryptFromBase64String(dataItem.AccountNumber);
            dataItem.SortCode = _encryptionService.DecryptFromBase64String(dataItem.SortCode);
        }

        private void Decrypt(DataItemCreditCard dataItem)
        {
            dataItem.Id = _encryptionService.DecryptFromBase64String(dataItem.Id);
            dataItem.Name = _encryptionService.DecryptFromBase64String(dataItem.Name);
            dataItem.Notes = _encryptionService.DecryptFromBase64String(dataItem.Notes);
            dataItem.CardType = _encryptionService.DecryptFromBase64String(dataItem.CardType);
            dataItem.CardNumber = _encryptionService.DecryptFromBase64String(dataItem.CardNumber);
            dataItem.SecurityCode = _encryptionService.DecryptFromBase64String(dataItem.SecurityCode);
            dataItem.ExpiryDate = _encryptionService.DecryptFromBase64String(dataItem.ExpiryDate);
            dataItem.Pin = _encryptionService.DecryptFromBase64String(dataItem.Pin);
        }

        private void Decrypt(AccountCredentials accountCredentials)
        {
            accountCredentials.Reference = _encryptionService.DecryptFromBase64String(accountCredentials.Reference);
            accountCredentials.UserName = _encryptionService.DecryptFromBase64String(accountCredentials.UserName);
            accountCredentials.Password = _encryptionService.DecryptFromBase64String(accountCredentials.Password);
        }

        private void Decrypt(CFDataLocker.Models.Contact contact)
        {
            contact.Email = _encryptionService.DecryptFromBase64String(contact.Email);
            contact.PhoneNumber = _encryptionService.DecryptFromBase64String(contact.PhoneNumber);
        }

        private void Decrypt(DataItemDocument dataItem)
        {
            dataItem.Id = _encryptionService.DecryptFromBase64String(dataItem.Id);
            dataItem.Name = _encryptionService.DecryptFromBase64String(dataItem.Name);
            dataItem.Notes = _encryptionService.DecryptFromBase64String(dataItem.Notes);
            dataItem.FilePath = _encryptionService.DecryptFromBase64String(dataItem.FilePath);
        }
    }
}

