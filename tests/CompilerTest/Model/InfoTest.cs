using System.Collections.Generic;
using Xunit;
using Compiler.Model;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class InfoTest
    {
        private readonly Info model;
        private InfoName infoName;
        private InfoCallsign infoCallsign;
        private InfoAirport infoAirport;
        private InfoLatitude infoLatitude;
        private InfoLongitude infoLongitude;
        private InfoMilesPerDegreeLatitude infoPerDegreeLatitude;
        private InfoMilesPerDegreeLongitude infoPerDegreeLongitude;
        private InfoMagneticVariation infoMagVar;
        private InfoScale infoScale;

        public InfoTest()
        {
            this.infoName = new InfoName(
                "Super Cool Sector",
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
            this.infoCallsign = new InfoCallsign(
                "LON_CTR",
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
            this.infoAirport = new InfoAirport(
                "EGLL",
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
            this.infoLatitude = new InfoLatitude(
                "123",
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
            this.infoLongitude = new InfoLongitude(
                "456",
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
            this.infoPerDegreeLatitude = new InfoMilesPerDegreeLatitude(
                60,
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
            this.infoPerDegreeLongitude = new InfoMilesPerDegreeLongitude(
                40.24,
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
            this.infoMagVar = new InfoMagneticVariation(
                2.1,
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
            this.infoScale = new InfoScale(
                1,
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
            this.model = new Info(
                this.infoName,
                this.infoCallsign,
                this.infoAirport,
                this.infoLatitude,
                this.infoLongitude,
                this.infoPerDegreeLatitude,
                this.infoPerDegreeLongitude,
                this.infoMagVar,
                this.infoScale
            );
        }

        [Fact]
        public void TestItSetsName()
        {
            Assert.Equal(this.infoName, this.model.Name);
        }

        [Fact]
        public void TestItSetsCallsign()
        {
            Assert.Equal(this.infoCallsign, this.model.Callsign);
        }

        [Fact]
        public void TestItSetsAirport()
        {
            Assert.Equal(this.infoAirport, this.model.Airport);
        }

        [Fact]
        public void TestItSetsLatitude()
        {
            Assert.Equal(this.infoLatitude, this.model.Latitude);
        }
        
        [Fact]
        public void TestItSetsLongitude()
        {
            Assert.Equal(this.infoLongitude, this.model.Longitude);
        }

        [Fact]
        public void TestItSetsMilesPerDegreeLatitude()
        {
            Assert.Equal(this.infoPerDegreeLatitude, this.model.MilesPerDegreeLatitude);
        }

        [Fact]
        public void TestItSetsMilesPerDegreeLongitude()
        {
            Assert.Equal(this.infoPerDegreeLongitude, this.model.MilesPerDegreeLongitude);
        }

        [Fact]
        public void TestItSetsMagneticVariation()
        {
            Assert.Equal(this.infoMagVar, this.model.MagneticVariation);
        }

        [Fact]
        public void TestItSetsScale()
        {
            Assert.Equal(this.infoScale, this.model.Scale);
        }

        [Fact]
        public void TestItCompiles()
        {
            IEnumerable<ICompilableElement> expected = new List<ICompilableElement>()
            {
                this.infoName,
                this.infoCallsign,
                this.infoAirport,
                this.infoLatitude,
                this.infoLongitude,
                this.infoPerDegreeLatitude,
                this.infoPerDegreeLongitude,
                this.infoMagVar,
                this.infoScale
            };
            Assert.Equal(expected, this.model.GetCompilableElements());
        }
    }
}