using System.Data.Entity.ModelConfiguration;
using MoviesDB.Business.Entities;

namespace MoviesDB.Data.Mapping.Location
{
    public class StateConfiguration : EntityTypeConfiguration<State>
    {
        public StateConfiguration()
        {
            HasKey(t => t.Id);
            Ignore(t => t.EntityId);

            HasMany(t => t.Counties).WithRequired(t => t.State).HasForeignKey(t => t.StateId).WillCascadeOnDelete(false);
            HasMany(t => t.Directors).WithRequired(t => t.State).HasForeignKey(t => t.StateId).WillCascadeOnDelete(false);
            HasMany(t => t.Actors).WithRequired(t => t.State).HasForeignKey(t => t.StateId).WillCascadeOnDelete(false);
        }
    }
}
