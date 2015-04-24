namespace Logstore.Adapters.NLog
{
    public interface ILogMessageSender
    {
        void SendMessage(string message);
    }
}