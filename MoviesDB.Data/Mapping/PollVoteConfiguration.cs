using System.Data.Entity.ModelConfiguration;
using MoviesDB.Business.Entities;

namespace MoviesDB.Data.Mapping
{
    public class PollVoteConfiguration : EntityTypeConfiguration<PollVote>
    {
        public PollVoteConfiguration()
        {
            HasKey(t => t.Id);
            Ignore(t => t.EntityId);

            HasMany(t => t.PollVotingRecords).WithRequired(t => t.PollVote)
                .HasForeignKey(t => t.VoteId).WillCascadeOnDelete(true);
        }
    }
}
