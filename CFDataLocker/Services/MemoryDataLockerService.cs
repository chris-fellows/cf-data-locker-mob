//using CFDataLocker.Interfaces;
//using CFDataLocker.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CFDataLocker.Services
//{
//    /// <summary>
//    /// Data locker stored in local file
//    /// </summary>
//    public class MemoryDataLockerService : IDataLockerService
//    {
//        private List<DataLocker> _dataLockers = new List<DataLocker>();

//        public MemoryDataLockerService()
//        {
//            _dataLockers = LoadAllForTesting();
//        }

//        public List<DataLocker> GetAll()
//        {
//            return _dataLockers;
//        }

//        private List<DataLocker> LoadAllForTesting()
//        {
//            var dataLocker1 = new DataLocker()
//            {
//                Id = Environment.UserName,
//                DataItems = new List<DataItem>()
//                {
//                    new DataItem()
//                    {
//                        Id = "1",
//                        Name = "eBay",
//                        Credentials = new AccountCredentials()
//                        {
//                            Reference = "chris.fellows",
//                            UserName = "myemail@hotmail.co.uk",
//                            Password = "abc123"
//                        },
//                        URL = "https://www.ebay.co.uk",
//                        Notes = "My eBay account notes"
//                    },
//                    new DataItem()
//                    {
//                        Id = "2",
//                        Name = "Facebook",
//                        Credentials = new AccountCredentials()
//                        {
//                            UserName = "myemail@hotmail.co.uk",
//                            Password = "xyz"
//                        },
//                        URL = "https://www.facebook.com",
//                        Notes = "My Facebook account notes"
//                    },                    
//                    new DataItem()
//                    {
//                        Id = "3",
//                        Name = "LinkedIn",
//                        Credentials = new AccountCredentials()
//                        {
//                            UserName = "myemail@hotmail.co.uk",
//                            Password = "99999"
//                        },
//                        URL = "https://www.linkedin.com",
//                        Notes = "My LinkedIn account notes"
//                    },
//                }
//            };
//            return new List<DataLocker>() { dataLocker1 };
//        }

//        public DataLocker GetById(string id)
//        {
//            return GetAll().FirstOrDefault(dl => dl.Id == id);
//        }

//        public DataLocker GetByUserName(string userName)
//        {
//            return GetAll().FirstOrDefault(dl => dl.UserName == userName);
//        }

//        public void Update(DataLocker dataLocker)
//        {
//            //throw new NotImplementedException();
//        }

//        public void Delete(string id)
//        {
//            _dataLockers.RemoveAll(dl => dl.Id == id);
//        }

//    }
//}
