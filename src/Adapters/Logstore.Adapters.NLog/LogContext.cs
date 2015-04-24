using System;

namespace Logstore.Adapters.NLog
{
    public class LogContext
    {
        public string LogLevel { get; set; }
        public string AppId { get; set; }
        public string AccessToken { get; set; }
        public string Id { get; set; }
        public DateTime Time { get; set; }
        public string Message { get; set; }
        public string MachineName { get; set; }
        public string ProcessId { get; set; }
        public string ThreadId { get; set; }
        public string Locale { get; set; }
    }
}