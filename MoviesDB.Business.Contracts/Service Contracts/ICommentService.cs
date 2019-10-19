using System.ServiceModel;
using MoviesDB.Business.Entities;

namespace MoviesDB.Business.Contracts
{
    [ServiceContract]
    public interface ICommentService
    {
        [OperationContract]
        Comment Get(int entityId);

        [OperationContract]
        Comment[] GetAll();

        [OperationContract]
        Comment Update(Comment entity);

        [OperationContract]
        void Delete(int entityId);

        [OperationContract]
        Comment[] GetByPage(int page, int pageSize);

        [OperationContract]
        int TotalCount();
    }
}
