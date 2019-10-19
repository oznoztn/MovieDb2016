using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Core.Common.Proxy
{
    public abstract class ProxyBase<T> : ClientBase<T> where T : class
    {
        protected ProxyBase()
        {
            //string userName = Thread.CurrentPrincipal.Identity.Name;

            //MessageHeader<string> header = new MessageHeader<string>(userName);

            //OperationContextScope contextScope =
            //    new OperationContextScope(InnerChannel);

            //OperationContext.Current.OutgoingMessageHeaders.Add(
            //    header.GetUntypedHeader("String", "System"));
        }
    }
}