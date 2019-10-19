using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Core.Common.Core;
using MoviesDB.Client.IoC;
using MoviesDb.IoC;

namespace MoviesDb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));

            CompositionContainer container = MEFLoader.Init(catalog.Catalogs);
            ObjectBase.Container = container;
            DependencyResolver.SetResolver(new MefDependencyResolver(container));
        }
    }
}
