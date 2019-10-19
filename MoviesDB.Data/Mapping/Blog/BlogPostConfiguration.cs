using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviesDB.Business.Entities;

namespace MoviesDB.Data.Mapping.Blog
{
    public class BlogPostConfiguration : EntityTypeConfiguration<BlogPost>
    {
        public BlogPostConfiguration()
        {
            HasKey(t => t.Id);
            Ignore(t => t.EntityId);
            
            HasMany(t => t.Comments)
                .WithRequired(t => t.BlogPost).HasForeignKey(t => t.BlogPostId).WillCascadeOnDelete(false);
        }
    }
}
