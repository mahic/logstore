using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Logstore.Adapters.NLog.Tests
{
    public static class MockServiceHostFactory
    {
        public static ServiceHost GenerateMockServiceHost<TMock>(TMock mock, string endpointAddress)
        {
            var baseAddress = new Uri(Bootstrapper.ServiceHostBaseUri);
            var serviceHost = new ServiceHost(mock, baseAddress);

            serviceHost.Description.Behaviors.Find<ServiceDebugBehavior>().IncludeExceptionDetailInFaults = true;
            serviceHost.Description.Behaviors.Find<ServiceBehaviorAttribute>().InstanceContextMode =
                InstanceContextMode.Single;
            serviceHost.AddServiceEndpoint(typeof (TMock), new BasicHttpBinding(), endpointAddress);

            return serviceHost;
        }
    }
}