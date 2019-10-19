using System.Data.Entity.ModelConfiguration;
using MoviesDB.Business.Entities;

namespace MoviesDB.Data.Mapping
{
    public class NewsCategoryConfiguration : EntityTypeConfiguration<NewsCategory>
    {
        public NewsCategoryConfiguration()
        {
            HasKey(t => t.Id);
            Ignore(t => t.EntityId);

            HasMany(t => t.NewsMappings)
                .WithRequired(t => t.NewsCategory)
                .HasForeignKey(t => t.NewsCategoryId)
                .WillCascadeOnDelete(false);

        }
    }
}
