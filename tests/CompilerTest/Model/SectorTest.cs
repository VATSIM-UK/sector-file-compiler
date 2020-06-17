using Xunit;
using Compiler.Model;
using System.Collections.Generic;

namespace CompilerTest.Model
{
    public class SectorTest
    {
        private readonly Sector model;
        private readonly SectorOwnerHierarchy owners;
        private readonly List<SectorAlternateOwnerHierarchy> altOwners = new List<SectorAlternateOwnerHierarchy>()
        {
            new SectorAlternateOwnerHierarchy("ALT1", new List<string>(){"LON1", "LON2"}, ""),
            new SectorAlternateOwnerHierarchy("ALT2", new List<string>(){"LON3", "LON4"}, ""),
        };
        private readonly List<SectorGuest> guests = new List<SectorGuest>()
        {
            new SectorGuest("LLN", "EGLL", "EGWU", ""),
            new SectorGuest("LLS", "EGLL", "*", ""),
        };
        private readonly List<SectorActive> active = new List<SectorActive>()
        {
            new SectorActive("EGLL", "09R", ""),
            new SectorActive("EGWU", "05", ""),
        };

        private readonly SectorBorder border;

        private SectorDepartureAirports departureAirports;

        private SectorArrivalAirports arrivalAirports;

        public SectorTest()
        {
            this.owners = new SectorOwnerHierarchy(
                new List<string>()
                {
                    "LLN", "LLS"
                },
                ""
            );

            this.border = new SectorBorder(
                new List<string>()
                {
                    "LINE1",
                    "LINE2",
                },
                ""
            );

            this.departureAirports = new SectorDepartureAirports(new List<string>() { "EGLL", "EGWU" }, "");
            this.arrivalAirports = new SectorArrivalAirports(new List<string>() { "EGSS", "EGGW" }, "");

            this.model = new Sector(
                "COOL",
                5000,
                66000,
                this.owners,
                this.altOwners,
                this.active,
                this.guests,
                this.border,
                this.arrivalAirports,
                this.departureAirports,
                "comment"
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
            Assert.Equal(this.border, this.model.Border);
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
        public void TestItCompiles()
        {
            Assert.Equal(
                "SECTOR:COOL:5000:66000 ;comment\r\n" +
                "OWNER:LLN:LLS\r\n" +
                "ALTOWNER:ALT1:LON1:LON2\r\n" +
                "ALTOWNER:ALT2:LON3:LON4\r\n" +
                "ACTIVE:EGLL:09R\r\n" +
                "ACTIVE:EGWU:05\r\n" +
                "GUEST:LLN:EGLL:EGWU\r\n" +
                "GUEST:LLS:EGLL:*\r\n" +
                "BORDER:LINE1:LINE2\r\n" +
                "ARRAPT:EGSS:EGGW\r\n" +
                "DEPAPT:EGLL:EGWU\r\n\r\n",
                this.model.Compile()
            );
        }
    }
}