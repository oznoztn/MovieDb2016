using System.ComponentModel.Composition;
using Core.Common.Proxy;
using MoviesDB.Client.Contracts;
using MoviesDB.Client.Entities;

namespace MoviesDB.Client.Proxies
{
    [Export(typeof(IPollVoteService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PollVoteProxy : ProxyBase<IPollVoteService>, IPollVoteService
    {
        public PollVote Get(int id)
        {
            return Channel.Get(id);
        }

        public PollVote[] GetAll()
        {
            return Channel.GetAll();
        }

        public PollVote Update(PollVote entity)
        {
            return Channel.Update(entity);
        }

        public void Delete(int id)
        {
            Channel.Delete(id);
        }

        public PollVote[] GetByPage(int page, int pageSize)
        {
            return Channel.GetByPage(page, pageSize);
        }

        public int TotalCount()
        {
            return Channel.TotalCount();
        }
    }
}
