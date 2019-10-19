using System.ComponentModel.Composition;
using Core.Common.Proxy;
using MoviesDB.Client.Contracts;
using MoviesDB.Client.Entities;

namespace MoviesDB.Client.Proxies
{
    [Export(typeof(IPollVotingRecordService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PollVotingRecordProxy : ProxyBase<IPollVotingRecordService>, IPollVotingRecordService
    {
        public PollVotingRecord Get(int id)
        {
            return Channel.Get(id);
        }

        public PollVotingRecord[] GetAll()
        {
            return Channel.GetAll();
        }

        public PollVotingRecord Update(PollVotingRecord entity)
        {
            return Channel.Update(entity);
        }

        public void Delete(int id)
        {
            Channel.Delete(id);
        }

        public PollVotingRecord[] GetByPage(int page, int pageSize)
        {
            return Channel.GetByPage(page, pageSize);
        }

        public int TotalCount()
        {
            return Channel.TotalCount();
        }
    }
}
