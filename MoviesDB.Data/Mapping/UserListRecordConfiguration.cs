using System.Data.Entity.ModelConfiguration;
using MoviesDB.Business.Entities;

namespace MoviesDB.Data.Mapping
{
    public class UserListRecordConfiguration : EntityTypeConfiguration<UserListRecord>
    {
        public UserListRecordConfiguration()
        {
            HasKey(t => t.Id);
            Ignore(t => t.EntityId);

        }
    }
}
