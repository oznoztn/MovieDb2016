using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Proxy;
using MoviesDB.Client.Contracts;
using MoviesDB.Client.Entities;

namespace MoviesDB.Client.Proxies.Service_Proxies
{
    [Export(typeof(ICountryService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CountryService : ProxyBase<ICountryService>, ICountryService
    {
        public Country Get(int countryId)
        {
            return Channel.Get(countryId);
        }

        public Country[] GetAll()
        {
            return Channel.GetAll();
        }

        public Country Update(Country country)
        {
            return Channel.Update(country);
        }

        public void Delete(int countryId)
        {
            Channel.Delete(countryId);
        }

        public Country[] GetByPage(int page, int pageSize)
        {
            return Channel.GetByPage(page, pageSize);
        }

        public int TotalCount()
        {
            return Channel.TotalCount();
        }

        public Country[] SearchCountry(string searchTerm)
        {
            return Channel.SearchCountry(searchTerm);
        }

        public string Statistics()
        {
            return Channel.Statistics();
        }
    }
}
