using Core.Common.Proxy;
using MoviesDB.Client.Contracts;
using MoviesDB.Client.Entities;
using System.ComponentModel.Composition;

namespace MoviesDB.Client.Proxies
{
    [Export(typeof(IGenreService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GenreProxy : ProxyBase<IGenreService>, IGenreService
    {
        public Genre Get(int id)
        {
            return Channel.Get(id);
        }

        public Genre[] GetAll()
        {
            return Channel.GetAll();
        }

        public Genre[] GetAllSubs()
        {
            return Channel.GetAllSubs();
        }

        public Genre[] FindByName(string genreName)
        {
            return Channel.FindByName(genreName);
        }

        public Genre Update(Genre entity)
        {
            return Channel.Update(entity);
        }

        public void Delete(int id)
        {
            Channel.Delete(id);
        }

        public Genre[] GetByPage(int page, int pageSize)
        {
            return Channel.GetByPage(page, pageSize);
        }

        public int TotalCount()
        {
            return Channel.TotalCount();
        }
    }
}
