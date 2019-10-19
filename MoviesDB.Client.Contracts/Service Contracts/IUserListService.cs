using Core.Common.Exceptions;
using System.Collections.Generic;
using System.ServiceModel;
using Core.Common.Contracts;
using MoviesDB.Client.Entities;
using MoviesDb.Common;

namespace MoviesDB.Client.Contracts
{
    [ServiceContract]
    public interface IUserListService : IServiceContract
    {
        [OperationContract]
        UserList Get(int entityId);

        [OperationContract]
        UserList[] GetAll();

        [OperationContract]
        UserList Update(UserList entity);

        [OperationContract]
        void Delete(int entityId);

        [OperationContract]
        [FaultContract(typeof(AuthorizationValidationException))]
        [FaultContract(typeof(NotFoundException))]
        IEnumerable<UserList> GetUserListsById(int userId);

        [OperationContract]
        [FaultContract(typeof(AuthorizationValidationException))]
        [FaultContract(typeof(NotFoundException))]
        IEnumerable<UserList> GetUserListsByEmail(string email);

        [OperationContract]
        [FaultContract(typeof(AuthorizationValidationException))]
        [FaultContract(typeof(NotFoundException))]
        UserList GetUserList(int listId);

        [OperationContract]
        Language[] GetByPage(int page, int pageSize);

        [OperationContract]
        int TotalCount();
    }
}
