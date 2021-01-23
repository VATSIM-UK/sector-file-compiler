using Xunit;
using Compiler.Model;
using System.Collections.Generic;
using System.Linq;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class SectorTest
    {
        private readonly Sector model;
        private readonly SectorOwnerHierarchy owners;
        private readonly List<SectorAlternateOwnerHierarchy> altOwners;
        private readonly List<SectorGuest> guests;
        private readonly List<SectorActive> active ;
        private readonly List<SectorBorder> borders;
        private readonly List<SectorDepartureAirports> departureAirports;
        private readonly List<SectorArrivalAirports> arrivalAirports;

        public SectorTest()
        {
            this.altOwners = SectorAlternateOwnerHierarchyFactory.MakeList();
            this.guests = SectorGuestFactory.MakeList();
            this.active = SectorActiveFactory.MakeList();
            this.owners = SectorOwnerHierarchyFactory.Make();
            this.borders = SectorBorderFactory.MakeList();
            this.departureAirports = SectorDepartureAirportsFactory.MakeList();
            this.arrivalAirports = SectorArrivalAirportsFactory.MakeList();

            this.model = new Sector(
                "COOL",
                5000,
                66000,
                this.owners,
                this.altOwners,
                this.active,
                this.guests,
                this.borders,
                this.arrivalAirports,
                this.departureAirports,
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
        }

        [Fact]
        public void TestItSetsSectorName()
        {
            Assert.Equal("COOL", this.model.Name);
        }

        [Fact]
        public void TestItSetsSectorMinimumAltitude()
        {
            Assert.Equal(5000, this.model.MinimumAltitude);
        }

        [Fact]
        public void TestItSetsSectorMaximumAltitude()
        {
            Assert.Equal(66000, this.model.MaximumAltitude);
        }

        [Fact]
        public void TestItSetsOwners()
        {
            Assert.Equal(this.owners, this.model.Owners);
        }

        [Fact]
        public void TestItSetsAltOwners()
        {
            Assert.Equal(this.altOwners, this.model.AltOwners);
        }

        [Fact]
        public void TestItSetsActive()
        {
            Assert.Equal(this.active, this.model.Active);
        }

        [Fact]
        public void TestItSetsGuests()
        {
            Assert.Equal(this.guests, this.model.Guests);
        }

        [Fact]
        public void TestItSetsBorder()
        {
            Assert.Equal(this.borders, this.model.Borders);
        }

        [Fact]
        public void TestItSetsArrivalAirports()
        {
            Assert.Equal(this.arrivalAirports, this.model.ArrivalAirports);
        }

        [Fact]
        public void TestItSetsDepartureAirports()
        {
            Assert.Equal(this.departureAirports, this.model.DepartureAirports);
        }

        [Fact]
        public void TestItReturnsCompilableElements()
        {
            IEnumerable<ICompilableElement> expected = new List<ICompilableElement>
                {
                    this.model,
                    this.model.Owners
                }
                .Concat(this.model.AltOwners)
                .Concat(this.model.Active)
                .Concat(this.model.Guests)
                .Concat(this.model.Borders)
                .Concat(this.model.ArrivalAirports)
                .Concat(this.model.DepartureAirports);
            
            Assert.Equal(expected, this.model.GetCompilableElements());
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal(
                "SECTOR:COOL:5000:66000",
                this.model.GetCompileData(new SectorElementCollection())
            );
        }
    }
}