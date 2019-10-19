using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Web.Mvc;

namespace MoviesDb.IoC
{
    public class MefDependencyResolver : IDependencyResolver
    {
        CompositionContainer _Container;

        public MefDependencyResolver(CompositionContainer container)
        {
            _Container = container;
        }

        public object GetService(Type serviceType)
        {
            return _Container.GetExportedValueByType(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _Container.GetExportedValuesByType(serviceType);
        }
    }
}