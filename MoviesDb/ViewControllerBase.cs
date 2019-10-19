using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.Mvc;
using Core.Common.Contracts;

namespace MoviesDb
{
    public class ViewControllerBase : Controller
    {        
        List<IServiceContract> _disposableServices;

        protected virtual void RegisterServices(List<IServiceContract> disposableServices)
        {
            // override edildiğinden bu kısımdaki kodlar çalımayacak.
        }

        List<IServiceContract> DisposableServices
        {
            get
            {
                if (_disposableServices == null)
                    _disposableServices = new List<IServiceContract>();

                return _disposableServices;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //base.OnActionExecuting(filterContext);

            RegisterServices(DisposableServices);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //base.OnActionExecuted(filterContext);

            foreach (var service in DisposableServices)
            {
                if (service != null && service is IDisposable)
                    (service as IDisposable).Dispose();
            }
        }

        protected void RunAndReleaseProxy<T>(T proxy, Action<T> codeToExecute)
        {
            codeToExecute.Invoke(proxy);

            IDisposable disposableProxy = proxy as IDisposable;
            if (disposableProxy != null)
            {
                disposableProxy.Dispose();
            }
        }
    }
}
