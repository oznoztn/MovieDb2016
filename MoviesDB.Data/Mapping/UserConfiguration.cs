using System.Data.Entity.ModelConfiguration;
using MoviesDB.Business.Entities;

namespace MoviesDB.Data.Mapping
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            HasKey(t => t.Id);
            Ignore(t => t.EntityId);

            Property(t => t.LastLoginDate).HasColumnType("datetime");
            Property(t => t.CreateDate).HasColumnType("datetime");

            HasMany(t => t.UserLists).WithRequired(t => t.User).HasForeignKey(t => t.UserId);
            HasMany(t => t.WatchlistRecords).WithRequired(t => t.User).HasForeignKey(t => t.UserId);
            HasMany(t => t.Comments).WithRequired(t => t.User).HasForeignKey(t => t.UserId);
        }
    }
}
