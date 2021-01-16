using Xunit;
using Compiler.Model;
using System.Collections.Generic;
using System.Linq;
using CompilerTest.Bogus;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class GeoTest
    {
        private readonly Geo model;
        private readonly List<GeoSegment> segments;
        private readonly Point firstPoint;
        private readonly Point secondPoint;

        public GeoTest()
        {
            this.secondPoint = PointFactory.Make();
            this.firstPoint = PointFactory.Make();
            this.segments = GeoSegmentFactory.MakeList(2);
            this.model = new Geo(
                "TestGeo",
                this.firstPoint,
                this.secondPoint,
                "red",
                this.segments,
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
        }

        [Fact]
        public void TestItSetsName()
        {
            Assert.Equal("TestGeo", this.model.Name);
        }

        [Fact]
        public void TestItSetsSegments()
        {
            Assert.Equal(this.segments, this.model.AdditionalSegments);
        }
        
        [Fact]
        public void TestItSetsFirstPoint()
        {
            Assert.Equal(this.firstPoint, this.model.FirstPoint);
        }
        
        [Fact]
        public void TestItSetsSecondPoint()
        {
            Assert.Equal(this.secondPoint, this.model.SecondPoint);
        }
        
        [Fact]
        public void TestItSetsColour()
        {
            Assert.Equal("red", this.model.Colour);
        }

        [Fact]
        public void TestItReturnsCompilableElements()
        {
            IEnumerable<ICompilableElement> expected = new List<ICompilableElement>
            {
                this.model
            }.Concat(this.model.AdditionalSegments);
            Assert.Equal(expected, this.model.GetCompilableElements());
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal(
                $"TestGeo                     {this.firstPoint} {this.secondPoint} red",
                this.model.GetCompileData(new SectorElementCollection())
            );
        }
    }
}
