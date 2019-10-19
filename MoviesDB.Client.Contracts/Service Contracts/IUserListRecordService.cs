using System.ServiceModel;
using Core.Common.Contracts;
using MoviesDB.Client.Entities;

namespace MoviesDB.Client.Contracts
{
    [ServiceContract]
    public interface IUserListRecordService : IServiceContract
    {
        [OperationContract]
        UserListRecord Get(int entityId);

        [OperationContract]
        UserListRecord[] GetAll();

        [OperationContract]
        UserListRecord Update(UserListRecord entity);

        [OperationContract]
        void Delete(int entityId);

        [OperationContract]
        Language[] GetByPage(int page, int pageSize);

        [OperationContract]
        int TotalCount();
    }
}
