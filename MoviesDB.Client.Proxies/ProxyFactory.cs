using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;
using Core.Common.Core;

namespace MoviesDB.Client.Proxies
{
    [Export(typeof(IProxyFactory))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProxyFactory : IProxyFactory
    {
        T IProxyFactory.CreateProxy<T>()
        {
            return ObjectBase.Container.GetExportedValue<T>();
        }
        //public T CreateProxy<T>()
        //{
        //    return ObjectBase.Container.GetExportedValue<T>();
        //}
    }
}

// bu pattern'e Service Locator denir.
