using Xunit;
using Moq;
using Compiler.Argument;
using Compiler.Output;

namespace CompilerTest.Output
{
    public class LoggerTest
    {
        private CompilerArguments arguments;

        private Mock<IOutputInterface> mockOutput;

        public LoggerTest()
        {
            this.arguments = new CompilerArguments();
            this.mockOutput = new Mock<IOutputInterface>();
        }

        private Logger GetLogger()
        {
            return new Logger(this.mockOutput.Object, this.arguments);
        }

        [Fact]
        public void TestItLogsDebugAtDebugVerbosity()
        {
            this.arguments.Verbosity = OutputVerbosity.Debug;

            this.GetLogger().Debug("test");
            this.mockOutput.Verify(foo => foo.WriteLine("test"), Times.Once());
        }

        [Fact]
        public void TestItDoesntLogDebugAtInfoVerbosity()
        {
            this.arguments.Verbosity = OutputVerbosity.Info;

            this.GetLogger().Debug("test");
            this.mockOutput.Verify(foo => foo.WriteLine("test"), Times.Never());
        }

        [Fact]
        public void TestItLogsInfoAtInfoVerbosity()
        {
            this.arguments.Verbosity = OutputVerbosity.Info;

            this.GetLogger().Info("test");
            this.mockOutput.Verify(foo => foo.WriteLine("test"), Times.Once());
        }

        [Fact]
        public void TestItDoesntLogInfoAtWarningVerbosity()
        {
            this.arguments.Verbosity = OutputVerbosity.Warning;

            this.GetLogger().Info("test");
            this.mockOutput.Verify(foo => foo.WriteLine("test"), Times.Never());
        }

        [Fact]
        public void TestItLogsWarningAtWarningVerbosity()
        {
            this.arguments.Verbosity = OutputVerbosity.Warning;

            this.GetLogger().Warning("test");
            this.mockOutput.Verify(foo => foo.WriteLine("test"), Times.Once());
        }

        [Fact]
        public void TestItDoesntLogWarningAtErrorVerbosity()
        {
            this.arguments.Verbosity = OutputVerbosity.Error;

            this.GetLogger().Warning("test");
            this.mockOutput.Verify(foo => foo.WriteLine("test"), Times.Never());
        }

        [Fact]
        public void TestItLogsErrorAtErrorVerbosity()
        {
            this.arguments.Verbosity = OutputVerbosity.Error;

            this.GetLogger().Error("test");
            this.mockOutput.Verify(foo => foo.WriteLine("test"), Times.Once());
        }

        [Fact]
        public void TestItDoesntLogErrorAtNullVerbosity()
        {
            this.arguments.Verbosity = OutputVerbosity.Null;

            this.GetLogger().Error("test");
            this.mockOutput.Verify(foo => foo.WriteLine("test"), Times.Never());
        }
    }
}
