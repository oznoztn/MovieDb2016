using System.Data.Entity.ModelConfiguration;
using MoviesDB.Business.Entities;

namespace MoviesDB.Data.Mapping
{
    public class ActorConfiguration : EntityTypeConfiguration<Actor>
    {
        public ActorConfiguration()
        {
            HasKey(t => t.Id);
            Ignore(t => t.EntityId);

            Property(t => t.BirthDate).HasColumnType("date");
            Property(t => t.DeathDate).HasColumnType("date");
            Property(t => t.CreatedAt).HasColumnType("date");

            HasRequired(t => t.Country).WithMany(t => t.Actors).HasForeignKey(t => t.CountryId).WillCascadeOnDelete(false);
            HasRequired(t => t.County).WithMany(t => t.Actors).HasForeignKey(t => t.CountyId).WillCascadeOnDelete(false);
            HasRequired(t => t.State).WithMany(t => t.Actors).HasForeignKey(t => t.StateId).WillCascadeOnDelete(false);


            
        }
    }
}
