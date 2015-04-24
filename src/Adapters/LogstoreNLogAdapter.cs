using Microsoft.Framework.ConfigurationModel;

namespace Logstore.Adapters.NLog
{
    public class LogstoreNLogAdapter
    {
        public int Port { get; private set; }
        public LogstoreNLogAdapter()
        {
            Init();
        }

        private void Init()
        {
            var configuration = new Configuration();
            configuration.AddJsonFile("logstore.json");
            var portString = configuration.Get("Agent:Port");
            int port = 0;
            int.TryParse(portString, out port);
            Port = port > 0 ? port : Defaults.Port;
        }
    }
}
