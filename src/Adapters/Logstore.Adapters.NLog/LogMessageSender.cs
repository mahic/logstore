using System.ServiceModel;

namespace Logstore.Adapters.NLog
{
    public class LogMessageSender : ILogMessageSender
    {
        private readonly int _port;
        private readonly ChannelFactory<ILogstoreAgentService> _channelFactory;

        public LogMessageSender(int port)
        {
            _port = port;

            var binding = new BasicHttpBinding();
            var endpoint = new EndpointAddress("http://localhost:" + _port + "/logstore");
            _channelFactory = new ChannelFactory<ILogstoreAgentService>(binding, endpoint);
        }

        public void SendMessage(string message)
        {
            ILogstoreAgentService client = null;

            try
            {
                client = _channelFactory.CreateChannel();
                client.Log(message);
                ((ICommunicationObject)client).Close();
            }
            catch
            {
                if (client != null)
                {
                    ((ICommunicationObject)client).Abort();
                }
            }
        }
    }
}