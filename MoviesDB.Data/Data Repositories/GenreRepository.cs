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
    [Export(typeof(IGenreRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GenreRepository : DataRepositoryBase<Genre>, IGenreRepository
    {
        protected override Genre AddEntity(MovieDbContext entityContext, Genre entity)
        {
            return entityContext.GenreSet.Add(entity);
        }

        protected override Genre UpdateEntity(MovieDbContext entityContext, Genre entity)
        {
            return (from e in entityContext.GenreSet
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override Genre GetEntity(MovieDbContext entityContext, int id)
        {
            return (from e in entityContext.GenreSet
                    where e.Id == id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Genre> GetEntities(MovieDbContext entityContext)
        {
            return entityContext.GenreSet.Where(t => t.IsSubGenre == false).ToList();
        }

        public IEnumerable<Genre> GetAllSubs()
        {
            List<Genre> genres;
            using (var entityContext = new MovieDbContext())
            {
                genres = entityContext.GenreSet.Where(t => t.IsSubGenre).ToList();
            }
            return genres;
        }

        public IEnumerable<Genre> GetByPage(int page, int pageSize)
        {
            using (var entityContext = new MovieDbContext())
            {
                return entityContext.GenreSet.OrderBy(t => t.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public Genre[] FindByName(string genreName)
        {
            using (var entityContext = new MovieDbContext())
            {
                return entityContext.GenreSet.Where(t => t.Name.ToLower().StartsWith(genreName)).ToArray();
            }
        }
    }
}
