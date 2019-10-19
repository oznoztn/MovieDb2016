using System.Data.Entity.ModelConfiguration;
using MoviesDB.Business.Entities;

namespace MoviesDB.Data.Mapping
{
    public class UserListConfiguration : EntityTypeConfiguration<UserList>
    {
        public UserListConfiguration()
        {
            HasKey(t => t.Id);
            Ignore(t => t.EntityId);

            HasMany(t => t.UserListRecords).WithRequired(t => t.UserList).HasForeignKey(t => t.ListId);
        }
    }
}
