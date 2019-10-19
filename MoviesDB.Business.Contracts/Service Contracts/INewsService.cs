using System.ServiceModel;
using MoviesDB.Business.Entities;

namespace MoviesDB.Business.Contracts
{
    [ServiceContract]
    public interface INewsService
    {
        [OperationContract]
        News Get(int entityId);

        [OperationContract]
        News[] GetAll();

        [OperationContract]
        News Update(News entity);

        [OperationContract]
        void Delete(int entityId);

        [OperationContract]
        News[] GetByPage(int page, int pageSize);

        [OperationContract]
        int TotalCount();
    }
}
