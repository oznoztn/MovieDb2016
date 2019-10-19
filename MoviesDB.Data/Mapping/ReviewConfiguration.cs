using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviesDB.Business.Entities;

namespace MoviesDB.Data.Mapping
{
    public class ReviewConfiguration : EntityTypeConfiguration<Review>
    {
        public ReviewConfiguration()
        {
            HasKey(t => t.Id);
            Ignore(t => t.EntityId);

            HasRequired(t => t.Movie)
                .WithMany(t => t.Reviews).HasForeignKey(t => t.MovieId);

            HasRequired(t => t.User).WithMany(t => t.MovieReviews)
                .HasForeignKey(t => t.UserId);

            Property(t => t.CreatedAt).HasColumnType("datetime");
        }
    }
}
