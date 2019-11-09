using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using Compiler.Input;
using Compiler.Parser;

namespace CompilerTest.Input
{
    public class InputLineReaderTest
    {
        private readonly Mock<IFileInterface> mockInput;

        private readonly InputLineReader reader;

        public InputLineReaderTest()
        {
            this.mockInput = new Mock<IFileInterface>();
            this.reader = new InputLineReader();
        }

        [Fact]
        public void TestItThrowsExceptionOnNonExistentFile()
        {
            this.mockInput.Setup(foo => foo.Exists()).Returns(false);
            Assert.Throws<ArgumentException>(() => this.reader.ReadInputLines(this.mockInput.Object));
        }

        [Fact]
        public void TestItReturnsExpectedData()
        {
            List<string> expectedLines = new List<string>(new string[] { "abc", "def" });
            SectorFormatData expected = new SectorFormatData(
                "foo.txt",
                expectedLines
            );

            this.mockInput.Setup(foo => foo.Exists()).Returns(true);
            this.mockInput.Setup(foo => foo.GetPath()).Returns("foo.txt");
            this.mockInput.Setup(foo => foo.GetAllLines()).Returns(expectedLines);
            Assert.Equal(expected, this.reader.ReadInputLines(this.mockInput.Object));
        }
    }
}
