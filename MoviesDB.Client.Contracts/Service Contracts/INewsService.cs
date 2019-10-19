using System.ServiceModel;
using Core.Common.Contracts;
using MoviesDB.Client.Entities;

namespace MoviesDB.Client.Contracts
{
    [ServiceContract]
    public interface INewsService : IServiceContract
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
