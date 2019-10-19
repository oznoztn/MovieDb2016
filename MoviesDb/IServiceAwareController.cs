using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Common.Contracts;

namespace MoviesDb
{
    public interface IServiceAwareController
    {
        void RegisterDisposableServices(List<IServiceContract> disposableServices);

        List<IServiceContract> DisposableServices { get; }
    }
}
