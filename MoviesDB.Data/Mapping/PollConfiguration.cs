using System.Data.Entity.ModelConfiguration;
using MoviesDB.Business.Entities;

namespace MoviesDB.Data.Mapping
{
    public class PollConfiguration : EntityTypeConfiguration<Poll>
    {
        public PollConfiguration()
        {
            HasKey(t => t.Id);
            Ignore(t => t.EntityId);

            Property(t => t.CreatedAt).HasColumnType("datetime");
            HasMany(t => t.PollVotes)
                .WithRequired(t => t.Poll)
                .HasForeignKey(t => t.PollId).WillCascadeOnDelete(true);

            
        }
    }
}
