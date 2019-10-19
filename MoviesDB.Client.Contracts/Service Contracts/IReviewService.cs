using System.ServiceModel;
using Core.Common.Contracts;
using MoviesDB.Client.Entities;

namespace MoviesDB.Client.Contracts
{
    [ServiceContract]
    public interface IReviewService : IServiceContract
    {
        [OperationContract]
        Review Get(int entityId);

        [OperationContract]
        Review[] GetAll();

        [OperationContract]
        Review Update(Review entity);

        [OperationContract]
        void Delete(int entityId);

        [OperationContract]
        Review[] GetByPage(int page, int pageSize);

        [OperationContract]
        int TotalCount();
    }
}