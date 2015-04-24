using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NLog;
using NSubstitute;
using Xunit;

namespace Logstore.Adapters.NLog.Tests
{
    public class LogstoreTargetTests
    {
        private readonly TestMessageSender _messageSender;
        private Logger _logger;

        public LogstoreTargetTests()
        {
            _messageSender = new TestMessageSender();
            LogInitializer.InitializeProgramatically(_messageSender);
            _logger = LogManager.GetCurrentClassLogger();
        }


        [Fact]
        public void Test()
        {
            const string expectedLogMessage = "test";
            _logger.Debug(expectedLogMessage);
            _messageSender.Message.Should().Contain(expectedLogMessage);
        }

        [Fact]
        public void Integrasjonstest()
        {
            LogInitializer.InitializeProgramatically(new LogMessageSender(12000));
            var mock = GenerateAgentServiceMock();

            Bootstrapper.OpenServiceHost(mock, "logstore");

            const string expectedLogMessage = "test";
            _logger.Debug(expectedLogMessage);


            mock.Received().Log(Arg.Any<string>());
        }

        [Fact]
        public void Ytelsestest()
        {
            LogInitializer.InitializeProgramatically(new LogMessageSender(12000));
            var mock = GenerateAgentServiceMock();
            Bootstrapper.OpenServiceHost(mock, "logstore");

            const string expectedLogMessage = "test";

            var logs = 1000;

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < logs; i++)
            {
                _logger.Debug(expectedLogMessage);
            }
            stopwatch.Stop();

            stopwatch.ElapsedMilliseconds.Should().BeLessOrEqualTo(3000);
        }

        private ILogstoreAgentService GenerateAgentServiceMock()
        {
            var mock = Substitute.For<ILogstoreAgentService>();
            return mock;
        }
    }
}
