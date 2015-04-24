namespace Logstore.Adapters.NLog.Tests
{
    internal class TestMessageSender : ILogMessageSender
    {
        public string Message { get; set; }

        public void SendMessage(string message)
        {
            Message = message;
        }
    }
}