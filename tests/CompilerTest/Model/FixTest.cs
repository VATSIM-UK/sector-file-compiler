using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Model;
using Xunit;

namespace CompilerTest.Model
{
    public class FixTest
    {
        private readonly Fix fix;

        public FixTest()
        {
            this.fix = new Fix("ADMAG", new Coordinate("abc", "def"), "comment");
        }

        [Fact]
        public void TestItSetsIdentifier()
        {
            Assert.Equal("ADMAG", this.fix.Identifier);
        }

        [Fact]
        public void TestItSetsCoordinate()
        {
            Assert.Equal(new Coordinate("abc", "def"), this.fix.Coordinate);
        }

        public void TestItCompiles()
        {
            Assert.Equal("ADMAG abc def ;comment", this.fix.Compile());
        }
    }
}
