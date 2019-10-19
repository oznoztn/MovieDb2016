using System.ServiceModel;
using Core.Common.Contracts;
using MoviesDB.Client.Entities;

namespace MoviesDB.Client.Contracts
{
    [ServiceContract]
    public interface ICommentService : IServiceContract
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
