using System.ServiceModel;

namespace Logstore.Adapters.NLog
{
    [ServiceContract]
    public interface ILogstoreAgentService
    {
        [OperationContract]
        void Log(string message);
    }
}