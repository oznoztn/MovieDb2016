using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviesDB.Business.Entities;
using MoviesDB.Data.Contracts;

namespace MoviesDB.Data
{
    [Export(typeof(ICommentRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CommentRepository : DataRepositoryBase<Comment>, ICommentRepository
    {
        protected override Comment AddEntity(MovieDbContext entityContext, Comment entity)
        {
            return entityContext.CommentSet.Add(entity);
        }

        protected override Comment UpdateEntity(MovieDbContext entityContext, Comment entity)
        {
            return (from e in entityContext.CommentSet
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override Comment GetEntity(MovieDbContext entityContext, int id)
        {
            return (from e in entityContext.CommentSet
                    where e.Id == id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Comment> GetEntities(MovieDbContext entityContext)
        {
            return entityContext.CommentSet.ToList();
        }

        public IEnumerable<Comment> GetByPage(int page, int pageSize)
        {
            using (var entityContext = new MovieDbContext())
            {
                return entityContext.CommentSet.OrderBy(t => t.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }
    }
}
