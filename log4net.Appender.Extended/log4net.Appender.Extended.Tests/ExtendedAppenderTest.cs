using System;
using System.Linq;
using System.Threading;
using Castle.Windsor;
using log4net.Appender.Extended.Tests.TestDoubles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace log4net.Appender.Extended.Tests
{
    [TestClass]
    public class ExtendedAppenderTest
    {
        private static IWindsorContainer _container;
        private static ILog _logger;
        private static IExtendedLogStore _doubleBufferingAppenderLogStore;
        private static IExtendedLogStore _appenderLogStore;
        private const int MaxTimeThreshold = 1000 * 5;
        private const int MaxItemThreshold = 10;
        private const int LoopMax = 27;
        private static int _firstLoopMax;
        private const int ExpectedInformationVariableCount = 1; // Because only CustomItem LevelMin = ALL
        private const int ExpectedErrorVariableCount = 6; // Because StackTrace is special parameter and just mapped to ExtendedLoggingEvent.StackTrace property...
        const string ExceptionText = "UnitTest Exception";
        const string MessageFormat = "Information Message: {0}";
        const string ErrorMessageFormat = "Error Message: {0} : UnitTest Exception";
        const string NullMessageFormat = "Information Message: {0} is null!";
        const string NullErrorMessageFormat = "Error Message: {0} : UnitTest Exception is null!";

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _firstLoopMax = LoopMax - (LoopMax % MaxItemThreshold);
            _logger = Log4NetContainer.Logger;
            _container = TestContainerSetup.Instance.WindsorContainer;
            _appenderLogStore = _container.Resolve<IExtendedLogStore>(GlobalConstants.AppenderLogStore);
            _doubleBufferingAppenderLogStore = _container.Resolve<IExtendedLogStore>(GlobalConstants.BufferingAppenderLogStore);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _doubleBufferingAppenderLogStore.Clear();
            _appenderLogStore.Clear();
        }

        [TestMethod]
        public void Test_info_logs_DoubleBufferingAppenderSkeleton()
        {
            var count = _doubleBufferingAppenderLogStore.Count();
            Assert.AreEqual(0, count);
            for (var i = 0; i < _firstLoopMax; i++)
            {
                _logger.Info(string.Format(MessageFormat, i));
            }
            Thread.Sleep(MaxTimeThreshold / 2);
            for (var i = _firstLoopMax; i < LoopMax; i++)
            {
                _logger.Info(string.Format(MessageFormat, i));
            }
            count = _doubleBufferingAppenderLogStore.Count();
            Assert.AreEqual(_firstLoopMax, count);
            for (var i = 0; i < _firstLoopMax; i++)
            {
                var item = _doubleBufferingAppenderLogStore.AsQueryable.FirstOrDefault(e => e.Message == string.Format(MessageFormat, i));
                Assert.IsNotNull(item, string.Format(NullMessageFormat, i));
                Assert.AreEqual(ExpectedInformationVariableCount, item.Variables.Count);
            }
            Thread.Sleep(MaxTimeThreshold);
            for (var i = _firstLoopMax; i < LoopMax; i++)
            {
                var item = _doubleBufferingAppenderLogStore.AsQueryable.FirstOrDefault(e => e.Message == string.Format(MessageFormat, i));
                Assert.IsNotNull(item, string.Format(NullMessageFormat, i));
                Assert.AreEqual(ExpectedInformationVariableCount, item.Variables.Count);
            }
        }

        [TestMethod]
        public void Test_error_logs_DoubleBufferingAppenderSkeleton()
        {
            var count = _doubleBufferingAppenderLogStore.Count();
            Assert.AreEqual(0, count);
            for (var i = 0; i < _firstLoopMax; i++)
            {
                _logger.Error(string.Format(ErrorMessageFormat, i), new Exception(ExceptionText));
            }
            Thread.Sleep(MaxTimeThreshold / 5);
            for (var i = _firstLoopMax; i < LoopMax; i++)
            {
                _logger.Error(string.Format(ErrorMessageFormat, i), new Exception(ExceptionText));
            }
            count = _doubleBufferingAppenderLogStore.Count();
            Assert.AreEqual(_firstLoopMax, count);
            for (var i = 0; i < _firstLoopMax; i++)
            {
                var item = _doubleBufferingAppenderLogStore.AsQueryable.FirstOrDefault(e => e.Message == string.Format(ErrorMessageFormat, i));
                Assert.IsNotNull(item, string.Format(NullErrorMessageFormat, i));
                Assert.AreEqual(ExpectedErrorVariableCount, item.Variables.Count);
            }
            Thread.Sleep(MaxTimeThreshold);
            for (var i = _firstLoopMax; i < LoopMax; i++)
            {
                var item = _doubleBufferingAppenderLogStore.AsQueryable.FirstOrDefault(e => e.Message == string.Format(ErrorMessageFormat, i));
                Assert.IsNotNull(item, string.Format(NullErrorMessageFormat, i));
                Assert.AreEqual(ExpectedErrorVariableCount, item.Variables.Count);
            }
        }

        [TestMethod]
        public void Test_info_logs_AppenderSkeleton()
        {
            for (var i = 0; i < LoopMax; i++)
            {
                _logger.Info(string.Format(MessageFormat, i));
            }
            Thread.Sleep(MaxTimeThreshold / 5);
            for (var i = 0; i < LoopMax; i++)
            {
                var item = _appenderLogStore.AsQueryable.FirstOrDefault(e => e.Message == string.Format(MessageFormat, i));
                Assert.IsNotNull(item, string.Format(NullMessageFormat, i));
                Assert.AreEqual(ExpectedInformationVariableCount, item.Variables.Count);
            }
        }

        [TestMethod]
        public void Test_error_logs_AppenderSkeleton()
        {
            for (var i = 0; i < LoopMax; i++)
            {
                _logger.Error(string.Format(ErrorMessageFormat, i), new Exception(ExceptionText));
            }
            Thread.Sleep(MaxTimeThreshold / 5);
            for (var i = 0; i < LoopMax; i++)
            {
                var item = _appenderLogStore.AsQueryable.FirstOrDefault(e => e.Message == string.Format(ErrorMessageFormat, i));
                Assert.IsNotNull(item, string.Format(NullErrorMessageFormat, i));
                Assert.AreEqual(ExpectedErrorVariableCount, item.Variables.Count);
            }
        }

    }
}
