using System.Data.Entity.ModelConfiguration;
using MoviesDB.Business.Entities;

namespace MoviesDB.Data.Mapping
{
    public class GenreConfiguration : EntityTypeConfiguration<Genre>
    {
        public GenreConfiguration()
        {
            HasKey(t => t.Id);
            Ignore(t => t.EntityId);

            //HasMany(t => t.Movies).WithMany(t => t.Genres)
            //    .Map(t =>
            //    {
            //        t.MapLeftKey("GenreId");
            //        t.MapRightKey("MovieId");
            //        t.ToTable("Map_GenreMovie");
            //    });
        }
    }
}
