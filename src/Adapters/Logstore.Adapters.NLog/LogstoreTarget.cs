using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using Newtonsoft.Json;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace Logstore.Adapters.NLog
{
    [Target("Logstore")]
    public sealed class LogstoreTarget : TargetWithLayout
    {
        public ILogMessageSender LogMessageSender { private get; set; }

        [DefaultValue(12000)]
        public int Port { get; set; }

        [RequiredParameter]
        public string AccessToken { get; set; }

        [RequiredParameter]
        public string AppId { get; set; }

        protected override void InitializeTarget()
        {
            base.InitializeTarget();
            if (LogMessageSender == null)
                LogMessageSender = new LogMessageSender(Port);
        }

        protected override void Write(LogEventInfo logEvent)
        {
            var sw = new Stopwatch();
            sw.Start();
            var logMessage = Layout.Render(logEvent);

            var context = new LogContext();
            context.Locale = Thread.CurrentThread.CurrentCulture.Name;
            context.MachineName = Environment.MachineName;
            context.ThreadId = Thread.CurrentThread.ManagedThreadId.ToString();
            context.ProcessId = Process.GetCurrentProcess().Id.ToString();
            context.LogLevel = logEvent.Level.Name;
            context.AccessToken = AccessToken;
            context.Id = Guid.NewGuid().ToString("N");
            context.AppId = AppId;
            context.Time = logEvent.TimeStamp;
            context.Message = logMessage;

            var message = JsonConvert.SerializeObject(context);
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            LogMessageSender.SendMessage(message);        
        }
    }
}
