using System.Data.Entity.ModelConfiguration;
using MoviesDB.Business.Entities;

namespace MoviesDB.Data.Mapping.Location
{
    public class CountryConfiguration : EntityTypeConfiguration<Country>
    {
        public CountryConfiguration()
        {
            HasKey(t => t.Id);
            Ignore(t => t.EntityId);

            HasMany(t => t.Actors).WithRequired(t => t.Country).HasForeignKey(t => t.CountryId).WillCascadeOnDelete(false);
            HasMany(t => t.Directors).WithRequired(t => t.Country).HasForeignKey(t => t.CountryId).WillCascadeOnDelete(false);
            HasMany(t => t.Movies).WithRequired(t => t.Country).HasForeignKey(t => t.CountryId).WillCascadeOnDelete(false);

        }
    }
}
