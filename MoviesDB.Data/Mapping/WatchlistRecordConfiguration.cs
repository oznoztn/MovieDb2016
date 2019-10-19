using System.Data.Entity.ModelConfiguration;
using MoviesDB.Business.Entities;

namespace MoviesDB.Data.Mapping
{
    public class WatchlistRecordConfiguration : EntityTypeConfiguration<WatchlistRecord>
    {
        public WatchlistRecordConfiguration()
        {
            HasKey(t => t.Id);
            Ignore(t => t.EntityId);

            Property(t => t.CreatedAt).HasColumnType("date");
        }
    }
}
