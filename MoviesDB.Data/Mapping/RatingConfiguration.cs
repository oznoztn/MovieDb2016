using System.Data.Entity.ModelConfiguration;
using MoviesDB.Business.Entities;

namespace MoviesDB.Data.Mapping
{
    public class RatingConfiguration : EntityTypeConfiguration<Rating>
    {
        public RatingConfiguration()
        {
            HasKey(t => t.Id);
            Ignore(t => t.EntityId);

            //Property(t => t.DateRated).HasColumnType("date");
        }
    }
}
