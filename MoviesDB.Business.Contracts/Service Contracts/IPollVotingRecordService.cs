using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using MoviesDB.Business.Entities;

namespace MoviesDB.Business.Contracts
{
    [ServiceContract]
    public interface IPollVotingRecordService
    {
        [OperationContract]
        PollVotingRecord Get(int entityId);

        [OperationContract]
        PollVotingRecord[] GetAll();

        [OperationContract]
        PollVotingRecord Update(PollVotingRecord entity);

        [OperationContract]
        void Delete(int entityId);

        [OperationContract]
        PollVotingRecord[] GetByPage(int page, int pageSize);

        [OperationContract]
        int TotalCount();
    }
}
