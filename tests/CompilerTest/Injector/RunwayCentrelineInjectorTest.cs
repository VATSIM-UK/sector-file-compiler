using Compiler.Injector;
using Compiler.Model;
using Xunit;

namespace CompilerTest.Injector
{
    public class RunwayCentrelineInjectorTest
    {
        private readonly SectorElementCollection sectorElementCollection = new();

        public RunwayCentrelineInjectorTest()
        {
            RunwayCentrelineInjector.InjectRunwayCentrelineData(sectorElementCollection);
        }

        [Fact]
        public void TestItInjectsRunwayCentrelines()
        {
            Assert.Single(sectorElementCollection.RunwayCentrelines);
            RunwayCentreline firstResult = sectorElementCollection.RunwayCentrelines[0];
            Assert.True(((CentrelineStarter) firstResult).IsExtended);
            Assert.Equal(
                new Coordinate("N000.00.00.000", "E000.00.00.000"),
                firstResult.CentrelineSegment.FirstCoordinate
            );
            
            Assert.Equal(
                new Coordinate("N000.00.00.000", "E000.00.00.000"),
                firstResult.CentrelineSegment.SecondCoordinate
            );
            
            Assert.Equal(new Definition("Defined by compiler", 0), firstResult.GetDefinition());
            Assert.Equal(new Docblock(), firstResult.Docblock);
            Assert.Equal(new Comment("Defined by compiler"), firstResult.InlineComment);
            
            // The fixed colour centreline should have the same base segment as the main one
            Assert.Single(sectorElementCollection.FixedColourRunwayCentrelines);
            RunwayCentreline secondResult = sectorElementCollection.FixedColourRunwayCentrelines[0];
            Assert.IsType<CentrelineStarter>(secondResult);
            Assert.False(((CentrelineStarter) secondResult).IsExtended);
            Assert.Equal(
                new Coordinate("N000.00.00.000", "E000.00.00.000"),
                secondResult.CentrelineSegment.FirstCoordinate
            );
            
            Assert.Equal(
                new Coordinate("N000.00.00.000", "E000.00.00.000"),
                secondResult.CentrelineSegment.SecondCoordinate
            );
            
            Assert.Equal(new Definition("Defined by compiler", 0), secondResult.GetDefinition());
            Assert.Equal(new Docblock(), secondResult.Docblock);
            Assert.Equal(new Comment("Defined by compiler"), secondResult.InlineComment);
        }
    }
}
