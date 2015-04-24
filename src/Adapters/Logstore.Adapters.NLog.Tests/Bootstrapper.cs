using System.ServiceModel;

namespace Logstore.Adapters.NLog.Tests
{
    public class Bootstrapper
    {
        public const string ServiceHostBaseUri = "http://localhost:12000";
        public static ServiceHost ServiceHost;

        public static void OpenServiceHost<T>(T serviceInstance, string endpointAddress)
        {
            if (ServiceHost != null && ServiceHost.State == CommunicationState.Opened)
                return;

            ServiceHost = MockServiceHostFactory.GenerateMockServiceHost(serviceInstance, endpointAddress);
            ServiceHost.Open();
        }

        public static void LukkServiceHost()
        {
            if (ServiceHost != null && ServiceHost.State == CommunicationState.Opened)
                ServiceHost.Close();
        }
    }
}