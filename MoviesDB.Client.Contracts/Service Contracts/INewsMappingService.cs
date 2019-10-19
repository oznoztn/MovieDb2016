using System.ServiceModel;
using Core.Common.Contracts;
using MoviesDB.Client.Entities;

namespace MoviesDB.Client.Contracts
{
    [ServiceContract]
    public interface INewsMappingService : IServiceContract
    {
        [OperationContract]
        NewsMapping Get(int entityId);

        [OperationContract]
        NewsMapping[] GetAll();

        [OperationContract]
        NewsMapping Update(NewsMapping entity);

        [OperationContract]
        void Delete(int entityId);

        [OperationContract]
        News[] GetByPage(int page, int pageSize);

        [OperationContract]
        int TotalCount();

    }
}