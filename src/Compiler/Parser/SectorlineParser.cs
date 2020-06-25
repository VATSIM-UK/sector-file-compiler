using System.Collections.Generic;
using Compiler.Model;
using Compiler.Error;
using Compiler.Event;
using System;

namespace Compiler.Parser
{
    public class SectorlineParser : AbstractSectorElementParser
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

        public override void ParseData(SectorFormatData data)
        {
            for (int i = 0; i < data.lines.Count;)
            {
                // Defer all metadata lines to the base
                if (this.ParseMetadata(data.lines[i]))
                {
                    continue;
                }

                SectorFormatLine sectorData = this.sectorLineParser.ParseLine(data.lines[i]);
                if (
                    sectorData.dataSegments[0] != "CIRCLE_SECTORLINE" &&
                    sectorData.dataSegments[0] != "SECTORLINE"
                ) {
                    this.errorLog.AddEvent(
                        new SyntaxError("Invalid SECTORLINE declaration", data.fullPath, i + 1)
                    );
                    return;
                }

                try
                {
                    i += sectorData.dataSegments[0] == "CIRCLE_SECTORLINE"
                        ? ParseCircleSectorline(ref data, i)
                        : ParseStandardSectorline(ref data, i);
                } catch
                {
                    // Logging dealt with higher-up
                    return;
                }

            }
        }

        private int ParseCircleSectorline(ref SectorFormatData data, int startLine)
        {
            // Deal with the declaration line
            SectorFormatLine declarationLine = this.sectorLineParser.ParseLine(data.lines[startLine]);

            if (declarationLine.dataSegments.Count != 4 && declarationLine.dataSegments.Count != 5)
            {
                this.errorLog.AddEvent(
                    new SyntaxError("Incorrect number of segments for SECTORLINE declaration", data.fullPath, startLine + 1)
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
                        new SyntaxError("Invalid CIRCLE_SECTORLINE coordinate", data.fullPath, startLine + 1)
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
                    new SyntaxError("Invalid CIRCLE_SECTORLINE radius", data.fullPath, startLine + 1)
                );
                throw new Exception();
            }


            List<SectorlineDisplayRule> displayRules = new List<SectorlineDisplayRule>();
            int i = startLine + 1;
            while (i < data.lines.Count)
            {
                // Defer all metadata lines to the base
                if (this.ParseMetadata(data.lines[i]))
                {
                    continue;
                }

                SectorFormatLine displayData = this.sectorLineParser.ParseLine(data.lines[i]);

                if (IsNewDeclarationLine(displayData))
                {
                    break;
                }

                try
                {
                    displayRules.Add(this.ParseDisplayRule(displayData));
                } catch (Exception exception)
                {
                    this.errorLog.AddEvent(
                        new SyntaxError(exception.Message, data.fullPath, i + 1)
                    );
                    throw exception;
                }

                i++;
            }

            if (displayRules.Count == 0)
            {
                this.errorLog.AddEvent(
                    new SyntaxError("No display rules found for SECTORLINE ", data.fullPath, i + 1)
                );
                throw new Exception();
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

            return i - startLine;
        }

        private int ParseStandardSectorline(ref SectorFormatData data, int startLine)
        {
            // Deal with the declaration line
            SectorFormatLine declarationLine = this.sectorLineParser.ParseLine(data.lines[startLine]);

            if (declarationLine.dataSegments.Count != 2)
            {
                this.errorLog.AddEvent(
                    new SyntaxError("Incorrect number of segments for SECTORLINE declaration", data.fullPath, startLine + 1)
                );
                throw new Exception();
            }

            List<SectorlineDisplayRule> displayRules = new List<SectorlineDisplayRule>();
            List<SectorlineCoordinate> coordinates = new List<SectorlineCoordinate>();
            int i = startLine + 1;
            while (i < data.lines.Count)
            {
                // Defer all metadata lines to the base
                if (this.ParseMetadata(data.lines[i]))
                {
                    continue;
                }

                SectorFormatLine dataLine = this.sectorLineParser.ParseLine(data.lines[i]);

                try
                {
                    if (IsNewDeclarationLine(dataLine))
                    {
                        // Declaration of new SECTORLINE, so stop here
                        break;
                    }
                    else if (IsCoordDeclaration(dataLine))
                    {
                        coordinates.Add(ParseCoordinate(dataLine));
                    }
                    else if (IsDiplayDeclaration(dataLine))
                    {
                        displayRules.Add(ParseDisplayRule(dataLine));
                    }
                    else
                    {
                        throw new Exception("Invalid line in SECTORLINE declaration");
                    }
                } catch (Exception exception)
                {
                    this.errorLog.AddEvent(
                        new SyntaxError(exception.Message, data.fullPath, i + 1)
                    );
                    throw exception;
                }

                i++;
            }

            if (displayRules.Count == 0)
            {
                this.errorLog.AddEvent(
                    new SyntaxError("No display rules found for SECTORLINE ", data.fullPath, i + 1)
                );
                throw new Exception();
            }

            if (coordinates.Count == 0)
            {
                this.errorLog.AddEvent(
                    new SyntaxError("No coordinates found for SECTORLINE ", data.fullPath, i + 1)
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

            return i - startLine;
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

        private bool IsNewDeclarationLine(SectorFormatLine data)
        {
            return data.dataSegments.Count > 0 && (
                data.dataSegments[0] == "SECTORLINE" ||
                data.dataSegments[0] == "CIRCLE_SECTORLINE"
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
