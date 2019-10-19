using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Core.Common.Contracts;
using MoviesDB.Business.Entities;
using MoviesDB.Data.Contracts;

namespace MoviesDB.Data
{
    [Export(typeof(IPollVotingRecordRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PollVotingRecordRepository : DataRepositoryBase<PollVotingRecord>, IPollVotingRecordRepository
    {
        protected override PollVotingRecord AddEntity(MovieDbContext entityContext, PollVotingRecord entity)
        {
            return entityContext.PollVotingRecordSet.Add(entity);
        }

        protected override PollVotingRecord UpdateEntity(MovieDbContext entityContext, PollVotingRecord entity)
        {
            return (from en in entityContext.PollVotingRecordSet where en.Id == entity.Id select en).FirstOrDefault();
        }

        protected override PollVotingRecord GetEntity(MovieDbContext entityContext, int id)
        {
            return (from en in entityContext.PollVotingRecordSet where en.Id == id select en).FirstOrDefault();
        }

        protected override IEnumerable<PollVotingRecord> GetEntities(MovieDbContext entityContext)
        {
            return entityContext.PollVotingRecordSet.ToList();
        }
    }
}
