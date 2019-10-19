using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviesDB.Business.Entities;

namespace MoviesDB.Data.Mapping.Blog
{
    public class BlogPostCommentConfiguration : EntityTypeConfiguration<BlogPostComment>
    {
        public BlogPostCommentConfiguration()
        {
            HasKey(t => t.Id);
            Ignore(t => t.EntityId);

            HasRequired(t => t.User)
                .WithMany(t => t.BlogPostComments).HasForeignKey(t => t.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}
