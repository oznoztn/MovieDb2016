using System;
using System.Data.Entity.ModelConfiguration;
using MoviesDB.Business.Entities;

namespace MoviesDB.Data.Mapping
{
    public class MovieConfiguration : EntityTypeConfiguration<Movie>
    {
        public MovieConfiguration()
        {
            HasKey(t => t.Id);
            Ignore(t => t.EntityId);

            Property(t => t.CreatedAt).HasColumnType("date");

            HasMany(t => t.NewsMappings).WithOptional(t => t.Movie).HasForeignKey(t => t.MovieId).WillCascadeOnDelete(false);
        }
    }
}
