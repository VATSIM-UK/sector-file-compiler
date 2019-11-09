using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Model;
using Xunit;

namespace CompilerTest.Model
{
    public class CoordinateTest
    {
        private readonly Coordinate coordinate;

        public CoordinateTest()
        {
            this.coordinate = new Coordinate("abc", "def");
        }

        [Fact]
        public void TestItSetsLatitude()
        {
            Assert.Equal("abc", coordinate.latitude);
        }

        [Fact]
        public void TestItSetsLongitude()
        {
            Assert.Equal("def", coordinate.longitude);
        }

        [Fact]
        public void TestItRepresentsAsString()
        {
            Assert.Equal("abc def", coordinate.ToString());
        }
    }
}
