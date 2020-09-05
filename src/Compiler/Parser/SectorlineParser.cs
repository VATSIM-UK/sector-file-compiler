using System.Collections.Generic;
using Compiler.Model;
using Compiler.Error;
using Compiler.Event;
using System;

namespace Compiler.Parser
{
    public class SectorlineParser : AbstractEseAirspaceParser
    {
        private readonly ISectorLineParser sectorLineParser;
        private readonly SectorElementCollection sectorElements;
        private readonly IEventLogger errorLog;

        public SectorlineParser(
            MetadataParser metadataParser,
            ISectorLineParser sectorLineParser,
            SectorElementCollection sectorElements,
            IEventLogger errorLog
        ) : base(metadataParser)
        {
            this.sectorLineParser = sectorLineParser;
            this.sectorElements = sectorElements;
            this.errorLog = errorLog;
        }


        public void ParseData(List<(int, string)> lines, string filename)
        {
            SectorFormatLine sectorData = this.sectorLineParser.ParseLine(lines[0].Item2);
            if (
                sectorData.dataSegments[0] != "CIRCLE_SECTORLINE" &&
                sectorData.dataSegments[0] != "SECTORLINE"
            )
            {
                this.errorLog.AddEvent(
                    new SyntaxError("Invalid SECTORLINE declaration", filename, lines[0].Item1)
                );
                throw new Exception();
            }

            if (sectorData.dataSegments[0] == "CIRCLE_SECTORLINE")
            {
                ParseCircleSectorline(lines, filename);
            } else {
                ParseStandardSectorline(lines, filename);
            }
        }

        private void ParseCircleSectorline(List<(int, string)> lines, string filename)
        {
            // Deal with the declaration line
            SectorFormatLine declarationLine = this.sectorLineParser.ParseLine(lines[0].Item2);

            if (declarationLine.dataSegments.Count != 4 && declarationLine.dataSegments.Count != 5)
            {
                this.errorLog.AddEvent(
                    new SyntaxError("Incorrect number of segments for SECTORLINE declaration", filename, lines[0].Item1)
                );
                throw new Exception();
            }

            Coordinate parsedCoordinate = new Coordinate("", "");
            if (declarationLine.dataSegments.Count == 5)
            {
                parsedCoordinate = CoordinateParser.Parse(
                    declarationLine.dataSegments[2],
                    declarationLine.dataSegments[3]
                );

                if (parsedCoordinate.Equals(CoordinateParser.invalidCoordinate))
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Invalid CIRCLE_SECTORLINE coordinate", filename, lines[0].Item1)
                    );
                    throw new Exception();
                }
            }

            if (
                double.TryParse(
                    declarationLine.dataSegments.Count == 4 ? declarationLine.dataSegments[3] : declarationLine.dataSegments[4],
                    out double radius) == false)
            {
                this.errorLog.AddEvent(
                    new SyntaxError("Invalid CIRCLE_SECTORLINE radius", filename, lines[0].Item1)
                );
                throw new Exception();
            }


            List<SectorlineDisplayRule> displayRules = new List<SectorlineDisplayRule>();
            int i = 1;
            while (i < lines.Count)
            {
                // Defer all metadata lines to the base
                if (this.ParseMetadata(lines[i].Item2))
                {
                    i++;
                    continue;
                }

                SectorFormatLine displayData = this.sectorLineParser.ParseLine(lines[i].Item2);

                try
                {
                    displayRules.Add(this.ParseDisplayRule(displayData));
                } catch (Exception exception)
                {
                    this.errorLog.AddEvent(
                        new SyntaxError(exception.Message, filename, lines[i].Item1)
                    );
                    throw exception;
                }

                i++;
            }

            // Add the right type of circle sectorline
            if (declarationLine.dataSegments.Count == 4)
            {
                this.sectorElements.Add(
                    new CircleSectorline(
                        declarationLine.dataSegments[1],
                        declarationLine.dataSegments[2],
                        radius,
                        displayRules,
                        declarationLine.comment
                    )
                );
            } else {
                this.sectorElements.Add(
                    new CircleSectorline(
                        declarationLine.dataSegments[1],
                        parsedCoordinate,
                        radius,
                        displayRules,
                        declarationLine.comment
                    )
                );
            }

        }

        private void ParseStandardSectorline(List<(int, string)> lines, string filename)
        {
            // Deal with the declaration line
            SectorFormatLine declarationLine = this.sectorLineParser.ParseLine(lines[0].Item2);

            if (declarationLine.dataSegments.Count != 2)
            {
                this.errorLog.AddEvent(
                    new SyntaxError("Incorrect number of segments for SECTORLINE declaration", filename, lines[0].Item1)
                );
                throw new Exception();
            }

            List<SectorlineDisplayRule> displayRules = new List<SectorlineDisplayRule>();
            List<SectorlineCoordinate> coordinates = new List<SectorlineCoordinate>();
            int i = 1;
            while (i < lines.Count)
            {
                // Defer all metadata lines to the base
                if (this.ParseMetadata(lines[i].Item2))
                {
                    i++;
                    continue;
                }

                SectorFormatLine dataLine = this.sectorLineParser.ParseLine(lines[i].Item2);

                try
                {
                    if (IsCoordDeclaration(dataLine))
                    {
                        coordinates.Add(ParseCoordinate(dataLine));
                    }
                    else if (IsDiplayDeclaration(dataLine))
                    {
                        displayRules.Add(ParseDisplayRule(dataLine));
                    }
                    else
                    {
                        throw new Exception("Invalid declaration in SECTORLINE declaration");
                    }
                } catch (Exception exception)
                {
                    this.errorLog.AddEvent(
                        new SyntaxError(exception.Message, filename, lines[0].Item1)
                    );
                    throw exception;
                }

                i++;
            }

            if (coordinates.Count == 0)
            {
                this.errorLog.AddEvent(
                    new SyntaxError("No coordinates found for SECTORLINE ", filename, lines[0].Item1)
                );
                throw new Exception();
            }

            this.sectorElements.Add(
                new Sectorline(
                    declarationLine.dataSegments[1],
                    displayRules,
                    coordinates,
                    declarationLine.comment
                )
            );
        }

        private SectorlineDisplayRule ParseDisplayRule(SectorFormatLine data)
        {
            if (data.dataSegments.Count != 4)
            {
                throw new Exception("Invalid number of SECTORLINE DISPLAY rule segements");
            }

            return new SectorlineDisplayRule(
                data.dataSegments[1],
                data.dataSegments[2],
                data.dataSegments[3],
                data.comment
            );
        }

        private SectorlineCoordinate ParseCoordinate(SectorFormatLine data)
        {
            if (data.dataSegments.Count != 3)
            {
                throw new Exception("Invalid number of SECTORLINE COORD segements");
            }

            Coordinate parsedCoordinate = CoordinateParser.Parse(
                data.dataSegments[1],
                data.dataSegments[2]
            );

            if (parsedCoordinate.Equals(CoordinateParser.invalidCoordinate))

            {
                throw new Exception("Invalid SECTORLINE coordinate");
            }

            return new SectorlineCoordinate(
                parsedCoordinate,
                data.comment
            );
        }

        private bool IsDiplayDeclaration(SectorFormatLine data)
        {
            return data.dataSegments.Count > 0 && data.dataSegments[0] == "DISPLAY";
        }

        private bool IsCoordDeclaration(SectorFormatLine data)
        {
            return data.dataSegments.Count > 0 && data.dataSegments[0] == "COORD";
        }
    }
}
