using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Core.Common.Contracts;
using MoviesDB.Business.Entities;
using MoviesDB.Data.Contracts;

namespace MoviesDB.Data
{
    [Export(typeof(IPollVoteRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PollVoteRepository : DataRepositoryBase<PollVote>, IPollVoteRepository
    {
        protected override PollVote AddEntity(MovieDbContext entityContext, PollVote entity)
        {
            return entityContext.PollVoteSet.Add(entity);
        }

        protected override PollVote UpdateEntity(MovieDbContext entityContext, PollVote entity)
        {
            return (from en in entityContext.PollVoteSet where en.Id == entity.Id select en).FirstOrDefault();
        }

        protected override PollVote GetEntity(MovieDbContext entityContext, int id)
        {
            return (from en in entityContext.PollVoteSet where en.Id == id select en).FirstOrDefault();
        }

        protected override IEnumerable<PollVote> GetEntities(MovieDbContext entityContext)
        {
            return entityContext.PollVoteSet.ToList();
        }
    }
}
