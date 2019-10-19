using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Core.Common.Contracts;
using MoviesDB.Business.Entities;
using MoviesDB.Data.Contracts;

namespace MoviesDB.Data
{
    [Export(typeof(IBlogPostRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BlogPostRepository : DataRepositoryBase<BlogPost>, IBlogPostRepository
    {
        protected override BlogPost AddEntity(MovieDbContext entityContext, BlogPost entity)
        {
            return entityContext.BlogPostSet.Add(entity);
        }

        protected override BlogPost UpdateEntity(MovieDbContext entityContext, BlogPost entity)
        {
            return (from m in entityContext.BlogPostSet
                    where m.Id == entity.Id
                    select m).FirstOrDefault();
        }

        protected override BlogPost GetEntity(MovieDbContext entityContext, int id)
        {
            return (from m in entityContext.BlogPostSet
                    where m.Id == id
                    select m).FirstOrDefault();
        }

        protected override IEnumerable<BlogPost> GetEntities(MovieDbContext entityContext)
        {
            return entityContext.BlogPostSet.ToList();
        }

        public IEnumerable<BlogPost> GetByPage(int page, int pageSize)
        {
            using (var entityContext = new MovieDbContext())
            {
                return entityContext.BlogPostSet.OrderBy(t => t.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }
    }
}
