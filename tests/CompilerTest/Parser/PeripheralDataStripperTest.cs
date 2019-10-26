using System.Collections.Generic;
using Xunit;
using Compiler.Parser;

namespace CompilerTest.Parser
{
    public class PeripheralDataStripperTest
    {
        [Fact]
        public void TestItHandlesNoLines()
        {
            Assert.Equal(new List<string>(), PeripheralDataStripper.StripPeripheralData(new List<string>()));
        }

        [Fact]
        public void TestItIgnoresBlankLines()
        {
            List<string> lines = new List<string>(new string[] { "\r\n", "\n" });
            Assert.Equal(new List<string>(), PeripheralDataStripper.StripPeripheralData(lines));
        }

        [Fact]
        public void TestItIgnoresBlankLinesWithTrim()
        {
            List<string> lines = new List<string>(new string[] { "   \r\n", "   \n" });
            Assert.Equal(new List<string>(), PeripheralDataStripper.StripPeripheralData(lines));
        }

        [Fact]
        public void TestItIgnoresCommentLines()
        {
            List<string> lines = new List<string>(new string[] { ";acomment\r\n" });
            Assert.Equal(new List<string>(), PeripheralDataStripper.StripPeripheralData(lines));
        }

        [Fact]
        public void TestItIgnoresCommentLinesWithTrim()
        {
            List<string> lines = new List<string>(new string[] { "   ;acomment\r\n" });
            Assert.Equal(new List<string>(), PeripheralDataStripper.StripPeripheralData(lines));
        }

        [Fact]
        public void TestItAddsLines()
        {
            List<string> lines = new List<string>(new string[] { "a really cool line \r\n" });
            Assert.Equal(
                new List<string>(new string[] { "a really cool line" }),
                PeripheralDataStripper.StripPeripheralData(lines)
            );
        }

        [Fact]
        public void TestItAddsLinesWithCommentsRemoved()
        {
            List<string> lines = new List<string>(new string[] { "a really cool line ;with a comment \r\n" });
            Assert.Equal(
                new List<string>(new string[] { "a really cool line" }),
                PeripheralDataStripper.StripPeripheralData(lines)
            );
        }
    }
}
