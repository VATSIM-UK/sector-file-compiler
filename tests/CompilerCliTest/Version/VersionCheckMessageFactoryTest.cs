using System;
using Xunit;
using CompilerCli.Version;

namespace CompilerCliTest.Version
{
    public class VersionCheckMessageFactoryTest
    {
        [Fact]
        public void TestItReturnsWarningWhenLatestIsNewer()
        {
            var current = new System.Version(1, 2, 3);
            string message = VersionCheckMessageFactory.BuildMessage(current, "1.3.0");

            Assert.Equal(
                "A newer version of the Sector File Compiler is available: 1.3.0 (you are running 1.2.3)." +
                Environment.NewLine +
                "Download it from https://github.com/VATSIM-UK/sector-file-compiler/releases/latest",
                message
            );
        }

        [Fact]
        public void TestItReturnsNullWhenLatestIsSame()
        {
            var current = new System.Version(1, 2, 3);
            Assert.Null(VersionCheckMessageFactory.BuildMessage(current, "1.2.3"));
        }

        [Fact]
        public void TestItReturnsNullWhenLatestIsOlder()
        {
            var current = new System.Version(1, 2, 3);
            Assert.Null(VersionCheckMessageFactory.BuildMessage(current, "1.0.0"));
        }

        [Fact]
        public void TestItReturnsNullWhenTagIsNull()
        {
            var current = new System.Version(1, 2, 3);
            Assert.Null(VersionCheckMessageFactory.BuildMessage(current, null));
        }

        [Fact]
        public void TestItReturnsNullWhenTagIsMalformed()
        {
            var current = new System.Version(1, 2, 3);
            Assert.Null(VersionCheckMessageFactory.BuildMessage(current, "not-a-version"));
        }

        [Fact]
        public void TestItReturnsNullWhenTagIsEmptyString()
        {
            var current = new System.Version(1, 2, 3);
            Assert.Null(VersionCheckMessageFactory.BuildMessage(current, ""));
        }

        [Fact]
        public void TestItIgnoresRevisionComponentOfCurrentVersion()
        {
            var current = new System.Version(1, 2, 3, 999);
            Assert.Null(VersionCheckMessageFactory.BuildMessage(current, "1.2.3"));
        }
    }
}
