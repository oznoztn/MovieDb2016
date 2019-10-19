using System.Collections.Generic;
using System.ComponentModel.Composition;
using Core.Common.Contracts;
using MoviesDB.Business.Entities;
using MoviesDB.Data.Contracts;
using System.Linq;

namespace MoviesDB.Data
{
    [Export(typeof(IBlogPostCommentRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BlogPostCommentRepository : DataRepositoryBase<BlogPostComment>, IBlogPostCommentRepository
    {
        protected override BlogPostComment AddEntity(MovieDbContext entityContext, BlogPostComment entity)
        {
            return entityContext.BlogPostCommentSet.Add(entity);
        }

        protected override BlogPostComment UpdateEntity(MovieDbContext entityContext, BlogPostComment entity)
        {
            return (from e in entityContext.BlogPostCommentSet where e.Id == entity.Id select e).FirstOrDefault();
        }

        protected override BlogPostComment GetEntity(MovieDbContext entityContext, int id)
        {
            return (from en in entityContext.BlogPostCommentSet where en.Id == id select en).FirstOrDefault();
        }

        protected override IEnumerable<BlogPostComment> GetEntities(MovieDbContext entityContext)
        {
            return entityContext.BlogPostCommentSet.ToList();
        }

        public IEnumerable<BlogPostComment> GetByPage(int page, int pageSize)
        {
            using (var entityContext = new MovieDbContext())
            {
                return entityContext.BlogPostCommentSet.OrderBy(t => t.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }
    }
}
