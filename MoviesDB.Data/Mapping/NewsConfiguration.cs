using System.Data.Entity.ModelConfiguration;
using MoviesDB.Business.Entities;

namespace MoviesDB.Data.Mapping
{
    public class NewsConfiguration : EntityTypeConfiguration<News>
    {
        public NewsConfiguration()
        {
            HasKey(t => t.Id);
            Ignore(t => t.EntityId);

            HasMany(t => t.NewsMappings).WithRequired(t => t.News)
                .HasForeignKey(t => t.NewsId)
                .WillCascadeOnDelete(false);
        }
    }
}
