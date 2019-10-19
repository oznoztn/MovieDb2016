using System.ServiceModel;
using MoviesDB.Business.Entities;

namespace MoviesDB.Business.Contracts
{
    [ServiceContract]
    public interface IReviewService
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