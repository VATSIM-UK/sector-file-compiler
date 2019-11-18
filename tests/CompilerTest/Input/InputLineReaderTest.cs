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

        public InputLineReaderTest()
        {
            this.mockInput = new Mock<IFileInterface>();
        }

        [Fact]
        public void TestItThrowsExceptionOnNonExistentFile()
        {
            this.mockInput.Setup(foo => foo.Exists()).Returns(false);
            Assert.Throws<ArgumentException>(() => InputLineReader.ReadInputLines(this.mockInput.Object));
        }

        [Fact]
        public void TestItReturnsExpectedData()
        {
            List<string> expectedLines = new List<string>(new string[] { "abc", "def" });
            SectorFormatData expected = new SectorFormatData(
                "foo.txt",
                "foo",
                "bar",
                expectedLines
            );

            this.mockInput.Setup(foo => foo.Exists()).Returns(true);
            this.mockInput.Setup(foo => foo.GetPath()).Returns("foo.txt");
            this.mockInput.Setup(foo => foo.GetNameWithoutExtension()).Returns("foo");
            this.mockInput.Setup(foo => foo.ParentFolder()).Returns("bar");
            this.mockInput.Setup(foo => foo.GetAllLines()).Returns(expectedLines);
            Assert.Equal(expected, InputLineReader.ReadInputLines(this.mockInput.Object));
        }
    }
}
