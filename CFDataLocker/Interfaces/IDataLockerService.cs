using CFDataLocker.Models;

namespace CFDataLocker.Interfaces
{
    /// <summary>
    /// Interface for data locker instances
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
