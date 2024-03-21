using CFDataLocker.Interfaces;
using CFDataLocker.Models;
using CFDataLocker.Utilities;
using System.Text;

namespace CFDataLocker.Services
{
    /// <summary>
    /// Service for storing data locker in XML format.
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

        public DataLocker GetById(string id)
        {
            var file = Path.Combine(_folder, $"{id}.locker");
            var dataLocker = XmlUtilities.DeserializeFromString<DataLocker>(File.ReadAllText(file, Encoding.UTF8));
            Decrypt(dataLocker);
            return dataLocker;
        }

        public DataLocker GetByUserName(string userName)
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
            dataLocker.UserName = _encryptionService.EncryptToBase64String(dataLocker.UserName);    //  Convert.ToBase64String(AesEncryptionUtilities.Encrypt(dataLocker.UserName, key, iv));
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
            }

            //dataLocker.DataItems.ForEach(dataItem => Encrypt(dataItem, iv, key));            
        }

        private void Encrypt(DataItemDefault dataItem)
        {
            dataItem.Id = _encryptionService.EncryptToBase64String(dataItem.Id);        //  Convert.ToBase64String(AesEncryptionUtilities.Encrypt(dataItem.Id, key, iv));
            dataItem.Name = _encryptionService.EncryptToBase64String(dataItem.Name);    // Convert.ToBase64String(AesEncryptionUtilities.Encrypt(dataItem.Name, key, iv));
            dataItem.Notes = _encryptionService.EncryptToBase64String(dataItem.Notes);  // Convert.ToBase64String(AesEncryptionUtilities.Encrypt(dataItem.Notes, key, iv));
            dataItem.URL = _encryptionService.EncryptToBase64String(dataItem.URL);      // Convert.ToBase64String(AesEncryptionUtilities.Encrypt(dataItem.URL, key, iv));

            Encrypt(dataItem.Contact);           
            Encrypt(dataItem.Credentials);
        }

        private void Encrypt(DataItemBankAccount dataItem)
        {
            dataItem.Id = _encryptionService.EncryptToBase64String(dataItem.Id);    //  Convert.ToBase64String(AesEncryptionUtilities.Encrypt(dataItem.Id, key, iv));
            dataItem.Name = _encryptionService.EncryptToBase64String(dataItem.Name); // Convert.ToBase64String(AesEncryptionUtilities.Encrypt(dataItem.Name, key, iv));
            dataItem.Notes = _encryptionService.EncryptToBase64String(dataItem.Notes);  // Convert.ToBase64String(AesEncryptionUtilities.Encrypt(dataItem.Notes, key, iv));
            dataItem.AccountName = _encryptionService.EncryptToBase64String(dataItem.AccountName);  // Convert.ToBase64String(AesEncryptionUtilities.Encrypt(dataItem.AccountName, key, iv));
            dataItem.AccountNumber = _encryptionService.EncryptToBase64String(dataItem.AccountNumber);  // Convert.ToBase64String(AesEncryptionUtilities.Encrypt(dataItem.AccountNumber, key, iv));
            dataItem.SortCode = _encryptionService.EncryptToBase64String(dataItem.SortCode);    // Convert.ToBase64String(AesEncryptionUtilities.Encrypt(dataItem.SortCode, key, iv));
        }

        private  void Encrypt(DataItemCreditCard dataItem)
        {
            dataItem.Id = _encryptionService.EncryptToBase64String(dataItem.Id); //  Convert.ToBase64String(AesEncryptionUtilities.Encrypt(dataItem.Id, key, iv));
            dataItem.Name = _encryptionService.EncryptToBase64String(dataItem.Name); // Convert.ToBase64String(AesEncryptionUtilities.Encrypt(dataItem.Name, key, iv));
            dataItem.Notes = _encryptionService.EncryptToBase64String(dataItem.Notes);  // Convert.ToBase64String(AesEncryptionUtilities.Encrypt(dataItem.Notes, key, iv));
            dataItem.CardType = _encryptionService.EncryptToBase64String(dataItem.CardType); //  Convert.ToBase64String(AesEncryptionUtilities.Encrypt(dataItem.CardType, key, iv));
            dataItem.CardNumber = _encryptionService.EncryptToBase64String(dataItem.CardNumber);  // Convert.ToBase64String(AesEncryptionUtilities.Encrypt(dataItem.CardNumber, key, iv));
            dataItem.SecurityCode = _encryptionService.EncryptToBase64String(dataItem.SecurityCode);  // Convert.ToBase64String(AesEncryptionUtilities.Encrypt(dataItem.SecurityCode, key, iv));
            dataItem.ExpiryDate = _encryptionService.EncryptToBase64String(dataItem.ExpiryDate);   // Convert.ToBase64String(AesEncryptionUtilities.Encrypt(dataItem.ExpiryDate, key, iv));
            dataItem.Pin = _encryptionService.EncryptToBase64String(dataItem.Pin);  // Convert.ToBase64String(AesEncryptionUtilities.Encrypt(dataItem.Pin, key, iv));
        }

        private void Encrypt(AccountCredentials accountCredentials)
        {
            accountCredentials.Reference = _encryptionService.EncryptToBase64String(accountCredentials.Reference);  // Convert.ToBase64String(AesEncryptionUtilities.Encrypt(accountCredentials.Reference, key, iv));
            accountCredentials.UserName = _encryptionService.EncryptToBase64String(accountCredentials.UserName);   // Convert.ToBase64String(AesEncryptionUtilities.Encrypt(accountCredentials.UserName, key, iv));
            accountCredentials.Password = _encryptionService.EncryptToBase64String(accountCredentials.Password);    // Convert.ToBase64String(AesEncryptionUtilities.Encrypt(accountCredentials.Password, key, iv));
        }

        private void Encrypt(CFDataLocker.Models.Contact contact)
        {
            contact.Email = _encryptionService.EncryptToBase64String(contact.Email);  // Convert.ToBase64String(AesEncryptionUtilities.Encrypt(contact.Email, key, iv));
            contact.PhoneNumber = _encryptionService.EncryptToBase64String(contact.PhoneNumber);    // Convert.ToBase64String(AesEncryptionUtilities.Encrypt(contact.PhoneNumber, key, iv));
        }

        private void Decrypt(DataLocker dataLocker)
        {
            dataLocker.UserName = _encryptionService.DecryptFromBase64String(dataLocker.UserName);  //  AesEncryptionUtilities.Decrypt(Convert.FromBase64String(dataLocker.UserName), key, iv);

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
            }

            //dataLocker.DataItems.ForEach(dataItem => Decrypt(dataItem, iv, key));           
        }

        private void Decrypt(DataItemDefault dataItem)
        {
            dataItem.Id = _encryptionService.DecryptFromBase64String(dataItem.Id);  //  AesEncryptionUtilities.Decrypt(Convert.FromBase64String(dataItem.Id), key, iv);
            dataItem.Name = _encryptionService.DecryptFromBase64String(dataItem.Name);  // AesEncryptionUtilities.Decrypt(Convert.FromBase64String(dataItem.Name), key, iv);
            dataItem.Notes = _encryptionService.DecryptFromBase64String(dataItem.Notes);  // AesEncryptionUtilities.Decrypt(Convert.FromBase64String(dataItem.Notes), key, iv);
            dataItem.URL = _encryptionService.DecryptFromBase64String(dataItem.URL);    // AesEncryptionUtilities.Decrypt(Convert.FromBase64String(dataItem.URL), key, iv);

            Decrypt(dataItem.Contact);
            Decrypt(dataItem.Credentials);
        }

        private  void Decrypt(DataItemBankAccount dataItem)
        {
            dataItem.Id = _encryptionService.DecryptFromBase64String(dataItem.Id);   // AesEncryptionUtilities.Decrypt(Convert.FromBase64String(dataItem.Id), key, iv);
            dataItem.Name = _encryptionService.DecryptFromBase64String(dataItem.Name);   // AesEncryptionUtilities.Decrypt(Convert.FromBase64String(dataItem.Name), key, iv);
            dataItem.Notes = _encryptionService.DecryptFromBase64String(dataItem.Notes);  // AesEncryptionUtilities.Decrypt(Convert.FromBase64String(dataItem.Notes), key, iv);
            dataItem.AccountName = _encryptionService.DecryptFromBase64String(dataItem.AccountName);   // AesEncryptionUtilities.Decrypt(Convert.FromBase64String(dataItem.AccountName), key, iv);
            dataItem.AccountNumber = _encryptionService.DecryptFromBase64String(dataItem.AccountNumber); // AesEncryptionUtilities.Decrypt(Convert.FromBase64String(dataItem.AccountNumber), key, iv);
            dataItem.SortCode = _encryptionService.DecryptFromBase64String(dataItem.SortCode);   // AesEncryptionUtilities.Decrypt(Convert.FromBase64String(dataItem.SortCode), key, iv);
        }

        private void Decrypt(DataItemCreditCard dataItem)
        {
            dataItem.Id = _encryptionService.DecryptFromBase64String(dataItem.Id);  // AesEncryptionUtilities.Decrypt(Convert.FromBase64String(dataItem.Id), key, iv);
            dataItem.Name = _encryptionService.DecryptFromBase64String(dataItem.Name);  // AesEncryptionUtilities.Decrypt(Convert.FromBase64String(dataItem.Name), key, iv);
            dataItem.Notes = _encryptionService.DecryptFromBase64String(dataItem.Notes);   // AesEncryptionUtilities.Decrypt(Convert.FromBase64String(dataItem.Notes), key, iv);
            dataItem.CardType = _encryptionService.DecryptFromBase64String(dataItem.CardType); // AesEncryptionUtilities.Decrypt(Convert.FromBase64String(dataItem.CardType), key, iv);
            dataItem.CardNumber = _encryptionService.DecryptFromBase64String(dataItem.CardNumber);   // AesEncryptionUtilities.Decrypt(Convert.FromBase64String(dataItem.CardNumber), key, iv);
            dataItem.SecurityCode = _encryptionService.DecryptFromBase64String(dataItem.SecurityCode); // AesEncryptionUtilities.Decrypt(Convert.FromBase64String(dataItem.SecurityCode), key, iv);
            dataItem.ExpiryDate = _encryptionService.DecryptFromBase64String(dataItem.ExpiryDate);  // AesEncryptionUtilities.Decrypt(Convert.FromBase64String(dataItem.ExpiryDate), key, iv);
            dataItem.Pin = _encryptionService.DecryptFromBase64String(dataItem.Pin);    // AesEncryptionUtilities.Decrypt(Convert.FromBase64String(dataItem.Pin), key, iv);
        }

        private void Decrypt(AccountCredentials accountCredentials)
        {
            accountCredentials.Reference = _encryptionService.DecryptFromBase64String(accountCredentials.Reference); // AesEncryptionUtilities.Decrypt(Convert.FromBase64String(accountCredentials.Reference), key, iv);
            accountCredentials.UserName = _encryptionService.DecryptFromBase64String(accountCredentials.UserName);  // AesEncryptionUtilities.Decrypt(Convert.FromBase64String(accountCredentials.UserName), key, iv);
            accountCredentials.Password = _encryptionService.DecryptFromBase64String(accountCredentials.Password);  // AesEncryptionUtilities.Decrypt(Convert.FromBase64String(accountCredentials.Password), key, iv);
        }

        private void Decrypt(CFDataLocker.Models.Contact contact)
        {
            contact.Email = _encryptionService.DecryptFromBase64String(contact.Email);  // AesEncryptionUtilities.Decrypt(Convert.FromBase64String(contact.Email), key, iv);
            contact.PhoneNumber = _encryptionService.DecryptFromBase64String(contact.PhoneNumber);  //  AesEncryptionUtilities.Decrypt(Convert.FromBase64String(contact.PhoneNumber), key, iv);
        }
    }
}

