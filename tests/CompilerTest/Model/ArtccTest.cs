using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Compiler.Model;

namespace CompilerTest.Model
{
    public class ArtccTest
    {
        private readonly Artcc artcc;

        public ArtccTest()
        {
            this.artcc = new Artcc(
                "EGTT",
                ArtccType.HIGH,
                new Coordinate("abc", "def"),
                new Coordinate("ghi", "jkl"),
                "comment"
            );
        }

        [Fact]
        public void TestItSetsIdentifier()
        {
            Assert.Equal("EGTT", this.artcc.Identifier);
        }

        [Fact]
        public void TestItSetsType()
        {
            Assert.Equal(ArtccType.HIGH, this.artcc.Type);
        }

        [Fact]
        public void TestItSetsStartCoordinate()
        {
            Assert.Equal(new Coordinate("abc", "def"), this.artcc.StartCoordinate);
        }

        [Fact]
        public void TestItSetsEndCoordinate()
        {
            Assert.Equal(new Coordinate("ghi", "jkl"), this.artcc.EndCoordinate);
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal("EGTT abc def ghi jkl ;comment\r\n", this.artcc.Compile());
        }
    }
}
