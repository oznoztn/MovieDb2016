using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviesDB.Business.Entities;

namespace MoviesDB.Data.Mapping
{
    public class NewsMappingConfiguration : EntityTypeConfiguration<NewsMapping>
    {
        public NewsMappingConfiguration()
        {
            Ignore(t => t.EntityId);
            HasKey(t => t.Id);

            HasRequired(t => t.News)
                .WithMany(t => t.NewsMappings)
                .HasForeignKey(t => t.NewsId)
                .WillCascadeOnDelete(false);

        //    HasOptional(t => t.Movie).WithMany(t => t.NewsMappings).WillCascadeOnDelete(false);
        //    HasOptional(t => t.Actor).WithMany(t => t.NewsMappings).WillCascadeOnDelete(false);
        //    HasOptional(t => t.Director).WithMany(t => t.NewsMappings).WillCascadeOnDelete(false);


        }
    }
}
