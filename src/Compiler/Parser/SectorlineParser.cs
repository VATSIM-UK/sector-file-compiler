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
                    i += sectorData.dataSegments[0] != "CIRCLE_SECTORLINE"
                        ? ParseCircleSectorline(ref data, i)
                        : ParseStandardSectorline(ref data, i);
                } catch (Exception exception)
                {
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

            if (declarationLine.dataSegments.Count != 5)
            {
                Coordinate parsedCoordinate = CoordinateParser.Parse(
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

            return i - startLine;
        }

        private int ParseStandardSectorline(ref SectorFormatData data, int startLine)
        {
            int i = startLine;
            while (i < data.lines.Count)
            {
                i++;
            }

            return i - startLine;
        }

        private SectorlineDisplayRule ParseDisplayRule(SectorFormatLine data)
        {
            if (data.dataSegments.Count != 4)
            {
                throw new Exception("Invalid number of SECTORLINE DISPLAY rule segements");
            }

            if (data.dataSegments[0] != "DISPLAY")
            {
                throw new Exception("SECTORLINE DISPLAY display segements must start with DISPLAY");
            }

            return new SectorlineDisplayRule(
                data.dataSegments[1],
                data.dataSegments[2],
                data.dataSegments[3],
                data.comment
            );
        }

        private bool IsNewDeclarationLine(SectorFormatLine data)
        {
            return data.dataSegments.Count > 0 && (
                data.dataSegments[0] == "SECTORLINE" ||
                data.dataSegments[0] == "CIRLC_ESECTORLINE"
            );
        }
    }
}
