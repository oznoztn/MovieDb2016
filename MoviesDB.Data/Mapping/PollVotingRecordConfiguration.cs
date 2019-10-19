using System.Data.Entity.ModelConfiguration;
using MoviesDB.Business.Entities;

namespace MoviesDB.Data.Mapping
{
    public class PollVotingRecordConfiguration : EntityTypeConfiguration<PollVotingRecord>
    {
        public PollVotingRecordConfiguration()
        {
            HasKey(t => t.Id);
            Ignore(t => t.EntityId);

            HasRequired(t => t.User)
                .WithMany(t => t.PollVotingRecords)
                .HasForeignKey(t => t.UserId);

            //HasRequired(t => t.PollVote)
            //    .WithMany(t => t.PollVotingRecords)
            //    .HasForeignKey(t => t.VoteId);
        }
    }
}
