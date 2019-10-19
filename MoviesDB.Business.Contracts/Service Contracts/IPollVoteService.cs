using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using MoviesDB.Business.Entities;

namespace MoviesDB.Business.Contracts
{
    [ServiceContract]
    public interface IPollVoteService
    {
        [OperationContract]
        PollVote Get(int entityId);

        [OperationContract]
        PollVote[] GetAll();

        [OperationContract]
        PollVote Update(PollVote entity);

        [OperationContract]
        void Delete(int entityId);

        [OperationContract]
        PollVote[] GetByPage(int page, int pageSize);

        [OperationContract]
        int TotalCount();
    }
}
