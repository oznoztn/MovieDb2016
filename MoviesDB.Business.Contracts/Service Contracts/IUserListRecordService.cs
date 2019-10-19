using System.ServiceModel;
using MoviesDB.Business.Entities;

namespace MoviesDB.Business.Contracts
{
    [ServiceContract]
    public interface IUserListRecordService
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
        UserListRecord[] GetByPage(int page, int pageSize);

        [OperationContract]
        int TotalCount();
    }
}
