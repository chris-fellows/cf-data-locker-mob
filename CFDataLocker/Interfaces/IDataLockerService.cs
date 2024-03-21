using CFDataLocker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDataLocker.Interfaces
{
    /// <summary>
    /// Interface for data locker
    /// </summary>
    public interface IDataLockerService
    {        
        //List<DataLocker> GetAll();  // TODO: Remove this (App should only access locker for current user)

        DataLocker GetById(string id);

        DataLocker GetByUserName(string userName);

        void Update(DataLocker dataLocker);

        void Delete(string id);
    }
}
