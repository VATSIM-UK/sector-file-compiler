using System;
using System.Collections.Generic;
using Compiler.Model;
using Compiler.Event;
using Compiler.Error;
using Compiler.Input;
using System.Linq;

namespace Compiler.Parser
{
    public class GeoParser : ISectorDataParser
    {
        private readonly SectorElementCollection elements;
        private readonly IEventLogger eventLogger;

        public GeoParser(
            SectorElementCollection elements,
            IEventLogger eventLogger
        ) {
            this.elements = elements;
            this.eventLogger = eventLogger;
        }

        public void ParseData(AbstractSectorDataFile data)
        {
            bool foundFirst = false;

            // Set up some variables for the first declaration line
            string name = "";
            Point initialFirstPoint = new Point("");
            Point initialSecondPoint = new Point("");
            string initialColour = "0";
            Definition initialDefinition = new Definition("", 1);
            Comment initialComment = new Comment("");
            Docblock initialDocblock = new Docblock();

            List<GeoSegment> segments = new List<GeoSegment>();
            foreach (SectorData line in data)
            {
                // If we haven't found the first item,
                if (!foundFirst)
                {
                    if (line.dataSegments.Count != 6)
                    {
                        this.eventLogger.AddEvent(
                            new SyntaxError("Expected name at start of GEO segment", line)
                        );
                        return;
                    }
                    name = line.dataSegments[0];

                    try
                    {
                        GeoSegment firstSegment = this.ParseGeoSegment(line, true);
                        initialFirstPoint = firstSegment.FirstPoint;
                        initialSecondPoint = firstSegment.SecondPoint;
                        initialColour = firstSegment.Colour;
                        initialDefinition = firstSegment.GetDefinition();
                        initialDocblock = firstSegment.Docblock;
                        initialComment = firstSegment.InlineComment;
                    } catch
                    {
                        // Syntax errors dealt with in segment parsing method
                        return;
                    }

                    foundFirst = true;
                    continue;
                }

                try
                {
                    segments.Add(this.ParseGeoSegment(line, false));
                } catch
                {
                    // Syntax errors dealt with in segment parsing method
                    return;
                }
            }

            // Add final geo element
            this.elements.Add(
                new Geo(
                    name,
                    initialFirstPoint,
                    initialSecondPoint,
                    initialColour,
                    segments,
                    initialDefinition,
                    initialDocblock,
                    initialComment
                )
            );
        }

        /*
         * Parses an individual GEO segment of coordinates and colours
         */
        private GeoSegment ParseGeoSegment(SectorData line, bool isFirstSegment)
        {
            // If it's the first item, it alwayus has a name attached so drop that off
            List<string> dataSegments = new List<string>();
            if (isFirstSegment)
            {
                dataSegments.RemoveAt(0);
            }

            if (dataSegments.Count != 5)
            {
                this.eventLogger.AddEvent(
                    new SyntaxError("Incorrect number parts for GEO segment", line)
                );
                throw new ArgumentException();
            }

            // Parse the first coordinate
            Point parsedStartPoint = PointParser.Parse(line.dataSegments[0], line.dataSegments[1]);
            if (parsedStartPoint.Equals(PointParser.invalidPoint))
            {
                this.eventLogger.AddEvent(
                    new SyntaxError("Invalid GEO segment point format", line)
                );
                throw new ArgumentException();
            }

            // Parse the end coordinate
            Point parsedEndPoint = PointParser.Parse(line.dataSegments[2], line.dataSegments[3]);
            if (parsedEndPoint.Equals(PointParser.invalidPoint))
            {
                this.eventLogger.AddEvent(
                    new SyntaxError("Invalid GEO segment point format", line)
                );
                throw new ArgumentException();
            }

            return new GeoSegment(
                parsedStartPoint,
                parsedEndPoint,
                line.dataSegments[4],
                line.definition,
                line.docblock,
                line.inlineComment
            );
        }
    }
}
