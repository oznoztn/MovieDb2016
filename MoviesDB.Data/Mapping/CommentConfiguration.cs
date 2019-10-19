using System.Data.Entity.ModelConfiguration;
using MoviesDB.Business.Entities;

namespace MoviesDB.Data.Mapping
{
    public class CommentConfiguration : EntityTypeConfiguration<Comment>
    {
        public CommentConfiguration()
        {
            HasKey(t => t.Id);
            Ignore(t => t.EntityId);

            Property(t => t.CreatedAt).HasColumnType("datetime");
        }
    }
}
