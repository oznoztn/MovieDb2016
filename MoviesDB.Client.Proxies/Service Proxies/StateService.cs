using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Proxy;
using MoviesDB.Client.Contracts;
using MoviesDB.Client.Entities;

namespace MoviesDB.Client.Proxies.Service_Proxies
{
    [Export(typeof(IStateService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class StateService : ProxyBase<IStateService>, IStateService
    {
        public State Get(int stateId)
        {
            return Channel.Get(stateId);
        }

        public State[] GetAll()
        {
            return Channel.GetAll();
        }

        public State[] GetStatesByCountry(int countryId)
        {
            return Channel.GetStatesByCountry(countryId);
        }

        public State Update(State state)
        {
            return Channel.Update(state);
        }

        public void Delete(int stateId)
        {
            Channel.Delete(stateId);
        }

        public State[] GetByPage(int page, int pageSize)
        {
            return Channel.GetByPage(page, pageSize);
        }

        public int TotalCount()
        {
            return Channel.TotalCount();
        }

    }
}
