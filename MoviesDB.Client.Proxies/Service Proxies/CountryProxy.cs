using Core.Common.Proxy;
using MoviesDB.Client.Contracts;
using MoviesDB.Client.Entities;
using System.ComponentModel.Composition;

namespace MoviesDB.Client.Proxies
{
    [Export(typeof(ILanguageService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CountryProxy : ProxyBase<ILanguageService>, ILanguageService
    {
        public Language Get(int entityId)
        {
            return Channel.Get(entityId);
        }

        public Language[] GetAll()
        {
            return Channel.GetAll();
        }

        public Language Update(Language country)
        {
            return Channel.Update(country);
        }

        public void Delete(int entityId)
        {
            Channel.Delete(entityId);
        }

        public Language[] GetByPage(int page, int pageSize)
        {
            return Channel.GetByPage(page, pageSize);
        }

        public int TotalCount()
        {
            return Channel.TotalCount();
        }
    }
}
