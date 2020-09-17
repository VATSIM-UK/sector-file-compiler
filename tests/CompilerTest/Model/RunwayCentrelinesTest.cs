using Xunit;
using Compiler.Model;
using System.Collections.Generic;

namespace CompilerTest.Model
{
    public class RunwayCentrelinesTest
    {
        private readonly RunwayCentrelines centrelines;

        public RunwayCentrelinesTest()
        {
            this.centrelines = new RunwayCentrelines(
                new List<Coordinate>
                {
                    new Coordinate("abc", "def"),
                    new Coordinate("ghi", "jkl"),
                },
                ""
            );
        }

        [Fact]
        public void TestItSetsCoordinates()
        {
            Assert.Equal(
                new List<Coordinate>
                {
                    new Coordinate("abc", "def"),
                    new Coordinate("ghi", "jkl"),
                },
                this.centrelines.Coordinates
            );
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal(
                "abc def\r\nghi jkl\r\n",
                this.centrelines.Compile()
            );
        }
    }
}
