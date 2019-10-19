using System.Data.Entity.ModelConfiguration;
using MoviesDB.Business.Entities;

namespace MoviesDB.Data.Mapping
{
    public class DirectorConfiguration : EntityTypeConfiguration<Director>
    {
        public DirectorConfiguration()
        {
            HasKey(t => t.Id);
            Ignore(t => t.EntityId);

            Property(t => t.BirthDate).HasColumnType("date");
            Property(t => t.DeathDate).HasColumnType("date");
            Property(t => t.CreatedAt).HasColumnType("date");

            HasRequired(t => t.Country).WithMany(t => t.Directors).HasForeignKey(t => t.CountryId).WillCascadeOnDelete(false);
            HasRequired(t => t.State).WithMany(t => t.Directors).HasForeignKey(t => t.StateId).WillCascadeOnDelete(false);
            HasRequired(t => t.County).WithMany(t => t.Directors).HasForeignKey(t => t.CountyId).WillCascadeOnDelete(false);
        }
    }
}
