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
    [Export(typeof(ICountyService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CountyService : ProxyBase<ICountyService>, ICountyService
    {
        public County Get(int countyId)
        {
            return Channel.Get(countyId);
        }

        public County[] GetAll()
        {
            return Channel.GetAll();
        }

        public County[] GetCountiesByState(int stateId)
        {
            return Channel.GetCountiesByState(stateId);
        }

        public County Update(County county)
        {
            return Channel.Update(county);
        }

        public void Delete(int countyId)
        {
            Channel.Delete(countyId);
        }

        public County[] GetByPage(int page, int pageSize)
        {
            return Channel.GetByPage(page, pageSize);
        }

        public int TotalCount()
        {
            return Channel.TotalCount();
        }
    }
}
