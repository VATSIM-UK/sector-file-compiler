using System.Collections.Generic;
using System.Linq;
using Xunit;
using Compiler.Model;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class RadarHoleTest
    {
        private readonly RadarHole modelNotNull;
        private readonly RadarHole modelNull;
        private readonly List<RadarHoleCoordinate> coordinates;

        public RadarHoleTest()
        {
            coordinates = new()
            {
                RadarHoleCoordinateFactory.Make(),
                RadarHoleCoordinateFactory.Make(),
                RadarHoleCoordinateFactory.Make()
            };
            
            modelNotNull = new RadarHole(
                1,
                2,
                3,
                coordinates,
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
            
            modelNull = new RadarHole(
                null,
                null,
                null,
                coordinates,
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
        }

        [Fact]
        public void TestItSetsPrimaryTopNotNull()
        {
            Assert.Equal(1, modelNotNull.PrimaryTop);
        }
        
        [Fact]
        public void TestItSetsPrimaryTopNull()
        {
            Assert.Null(modelNull.PrimaryTop);
        }
        
        [Fact]
        public void TestItSetsSModeTopNotNull()
        {
            Assert.Equal(2, modelNotNull.SModeTop);
        }
        
        [Fact]
        public void TestItSetsSModeTopNull()
        {
            Assert.Null(modelNull.SModeTop);
        }
        
        [Fact]
        public void TestItSetsCModeTopNotNull()
        {
            Assert.Equal(3, modelNotNull.CModeTop);
        }
        
        [Fact]
        public void TestItSetsCModeTopNull()
        {
            Assert.Null(modelNull.CModeTop);
        }
        
        [Fact]
        public void TestItSetsCoordinatesNotNull()
        {
            Assert.Equal(coordinates, modelNotNull.Coordinates);
        }
        
        [Fact]
        public void TestItSetsCoordinatesNull()
        {
            Assert.Equal(coordinates, modelNull.Coordinates);
        }

        [Fact]
        public void TestItCompilesNotNull()
        {
            Assert.Equal("HOLE:1:2:3", modelNotNull.GetCompileData(new SectorElementCollection()));
        }
        
        [Fact]
        public void TestItHasCompilableElementsNotNull()
        {
            Assert.Equal(
                new List<ICompilableElement>{modelNotNull}.Concat(coordinates),
                modelNotNull.GetCompilableElements()
            );
        }
        
        [Fact]
        public void TestItCompilesNull()
        {
            Assert.Equal("HOLE:::", modelNull.GetCompileData(new SectorElementCollection()));
        }
        
        [Fact]
        public void TestItHasCompilableElementsNull()
        {
            Assert.Equal(
                new List<ICompilableElement>{modelNull}.Concat(coordinates),
                modelNull.GetCompilableElements()
            );
        }
    }
}
