using System.ServiceModel;
using MoviesDB.Business.Entities;

namespace MoviesDB.Business.Contracts
{
    [ServiceContract]
    public interface IWatchlistRecordService
    {
        [OperationContract]
        WatchlistRecord Get(int entityId);

        [OperationContract]
        WatchlistRecord[] GetAll();

        [OperationContract]
        WatchlistRecord Update(Language entity);

        [OperationContract]
        void Delete(int entityId);

        [OperationContract]
        WatchlistRecord[] GetByPage(int page, int pageSize);

        [OperationContract]
        int TotalCount();
    }
}
