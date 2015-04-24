using System.Linq;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace Logstore.Adapters.NLog.Tests
{
    public class LogInitializer
    {
        public static void InitializeFromConfig()
        {
            var target = LogManager.Configuration.AllTargets.First();
            AddDefaultLoggingRule(target);
        }

        private static void AddDefaultLoggingRule(Target target)
        {
            var configuration = new LoggingConfiguration();
            var loggingRule = new LoggingRule("*", LogLevel.FromString("DEBUG"), target);
            configuration.LoggingRules.Add(loggingRule);
            LogManager.Configuration = configuration;
            LogManager.ReconfigExistingLoggers();
        }

        public static void InitializeProgramatically(ILogMessageSender sender)
        {
            ConfigurationItemFactory.Default.Targets.RegisterDefinition("Logstore", typeof (LogstoreTarget));
            var target = LogManager.Configuration.AllTargets.First() as LogstoreTarget;
            target.LogMessageSender = sender;
            AddDefaultLoggingRule(target);
        }
    }
}