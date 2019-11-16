﻿using System.Collections.Generic;
using Xunit;
using Moq;
using Compiler.Parser;
using Compiler.Error;
using Compiler.Model;
using Compiler.Event;
using Compiler.Output;

namespace CompilerTest.Parser
{
    public class ArtccParserTest
    {
        private readonly ArtccParser parser;

        private readonly SectorElementCollection collection;

        private readonly Mock<IEventLogger> log;

        public ArtccParserTest()
        {
            this.log = new Mock<IEventLogger>();
            this.collection = new SectorElementCollection();
            this.parser = (ArtccParser)(new SectionParserFactory(this.collection, this.log.Object))
                .GetParserForSection(OutputSections.SCT_ARTCC);
        }

        [Fact]
        public void TestItRaisesSyntaxErrorNotEnoughSegments()
        {
            SectorFormatData data = new SectorFormatData(
                "test",
                "EGHI",
                new List<string>(new string[] { "EGTT London FIR" })
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesSyntaxErrorInvalidEndCoordinate()
        {
            SectorFormatData data = new SectorFormatData(
                "test",
                "EGHI",
                new List<string>(new string[] { "EGTT London FIR N050.57.00.000 W001.21.24.490 N050.57.00.000 N001.21.24.490" })
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesSyntaxErrorInvalidStartCoordinate()
        {
            SectorFormatData data = new SectorFormatData(
                "test",
                "EGHI",
                new List<string>(new string[] { "EGTT London FIR N050.57.00.000 N001.21.24.490 N050.57.00.000 W001.21.24.490" })
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItHandlesMetadata()
        {
            SectorFormatData data = new SectorFormatData(
                "test",
                "test",
                new List<string>(new string[] { "" })
            );

            this.parser.ParseData(data);
            Assert.IsType<BlankLine>(this.collection.Compilables[OutputSections.SCT_ARTCC][0]);
        }

        [Fact]
        public void TestItAddsFixData()
        {
            SectorFormatData data = new SectorFormatData(
                "test",
                "EGHI",
                new List<string>(new string[] { "EGTT London FIR   N050.57.00.001 W001.21.24.490 N050.57.00.002 W001.21.24.490;comment" })
            );
            this.parser.ParseData(data);

            Artcc result = this.collection.Artccs[0];
            Assert.Equal("EGTT London FIR", result.Identifier);
            Assert.Equal(ArtccType.REGULAR, result.Type);
            Assert.Equal(new Coordinate("N050.57.00.001", "W001.21.24.490"), result.StartCoordinate);
            Assert.Equal(new Coordinate("N050.57.00.002", "W001.21.24.490"), result.EndCoordinate);
            Assert.Equal("comment", result.Comment);
        }
    }
}
