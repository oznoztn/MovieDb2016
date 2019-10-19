using System.ServiceModel;
using Core.Common.Contracts;
using MoviesDB.Client.Entities;

namespace MoviesDB.Client.Contracts
{
    [ServiceContract]
    public interface IWatchlistRecordService : IServiceContract
    {
        [OperationContract]
        WatchlistRecord Get(int entityId);

        [OperationContract]
        WatchlistRecord[] GetAll();

        [OperationContract]
        WatchlistRecord Update(WatchlistRecord entity);

        [OperationContract]
        void Delete(int entityId);

        [OperationContract]
        WatchlistRecord[] GetByPage(int page, int pageSize);

        [OperationContract]
        int TotalCount();
    }
}
