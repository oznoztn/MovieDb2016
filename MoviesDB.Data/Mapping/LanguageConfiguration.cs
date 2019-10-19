using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviesDB.Business.Entities;

namespace MoviesDB.Data.Mapping
{
    public class LanguageConfiguration : EntityTypeConfiguration<Language>
    {
        public LanguageConfiguration()
        {
            HasKey(t => t.Id);
            Ignore(t => t.EntityId);

            HasMany(t => t.Movies).WithRequired(t => t.Language).HasForeignKey(t => t.LanguageId);
        }
    }
}
