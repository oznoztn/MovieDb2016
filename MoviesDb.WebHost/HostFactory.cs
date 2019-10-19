using System;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace MoviesDb.WebHost
{
    public class HostFactory : ServiceHostFactory
    {
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            ServiceHost host = new ServiceHost(serviceType, baseAddresses);

            return host;
        }
    }
}