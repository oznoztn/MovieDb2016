using System.Data.Entity.ModelConfiguration;
using MoviesDB.Business.Entities;

namespace MoviesDB.Data.Mapping.Location
{
    public class CountyConfiguration : EntityTypeConfiguration<County>
    {
        public CountyConfiguration()
        {
            HasKey(t => t.Id);
            Ignore(t => t.EntityId);

            HasRequired(t => t.State).WithMany(t => t.Counties).HasForeignKey(t => t.StateId).WillCascadeOnDelete(false);
            HasMany(t => t.Actors).WithRequired(t => t.County).HasForeignKey(t => t.CountyId).WillCascadeOnDelete(false);
            HasMany(t => t.Directors).WithRequired(t => t.County).HasForeignKey(t => t.CountyId).WillCascadeOnDelete(false);
        }
    }
}
