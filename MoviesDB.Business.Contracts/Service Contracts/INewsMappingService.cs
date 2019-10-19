using System.ServiceModel;
using MoviesDB.Business.Entities;

namespace MoviesDB.Business.Contracts
{
    [ServiceContract]
    public interface INewsMappingService
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
        NewsMapping[] GetByPage(int page, int pageSize);

        [OperationContract]
        int TotalCount();

    }
}