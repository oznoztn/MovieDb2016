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
    [Export(typeof(IReviewRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ReviewRepository : DataRepositoryBase<Review>, IReviewRepository
    {
        protected override Review AddEntity(MovieDbContext entityContext, Review entity)
        {
            return entityContext.ReviewSet.Add(entity);
        }

        protected override Review UpdateEntity(MovieDbContext entityContext, Review entity)
        {
            return (from en in entityContext.ReviewSet where en.Id == entity.Id select en).FirstOrDefault();
        }

        protected override Review GetEntity(MovieDbContext entityContext, int id)
        {
            return (from en in entityContext.ReviewSet where en.Id == id select en).FirstOrDefault();
        }

        protected override IEnumerable<Review> GetEntities(MovieDbContext entityContext)
        {
            return entityContext.ReviewSet.ToList();
        }

        public IEnumerable<Review> GetByPage(int page, int pageSize)
        {
            using (var entityContext = new MovieDbContext())
            {
                return entityContext.ReviewSet.OrderBy(t => t.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }
    }
}
