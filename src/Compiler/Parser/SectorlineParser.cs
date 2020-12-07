using System.Collections.Generic;
using Compiler.Model;
using Compiler.Error;
using Compiler.Event;
using System;
using Compiler.Input;

namespace Compiler.Parser
{
    public class SectorlineParser : ISectorDataParser
    {
        private readonly SectorElementCollection sectorElements;
        private readonly IEventLogger errorLog;

        public SectorlineParser(
            SectorElementCollection sectorElements,
            IEventLogger errorLog
        ) {
            this.sectorElements = sectorElements;
            this.errorLog = errorLog;
        }

        public void ParseData(AbstractSectorDataFile data)
        {
            List<SectorData> linesToProcess = new List<SectorData>();
            bool foundFirst = false;
            foreach (SectorData line in data)
            {
                if (
                    !foundFirst &&
                    !this.IsNewDeclaration(line)
                ) {
                    {
                        this.errorLog.AddEvent(
                            new SyntaxError("Invalid SECTORLINE declaration", line)
                        );
                        return;
                    }
                }

                if (!foundFirst)
                {
                    linesToProcess.Add(line);
                    foundFirst = true;
                    continue;
                }

                if (this.IsNewDeclaration(line))
                {
                    try
                    {
                        this.ProcessLines(ref linesToProcess, data);
                    } catch
                    {
                        // Logging done higher up
                        return;
                    }
                    linesToProcess.Clear();
                }

                linesToProcess.Add(line);
            }

            try
            {
                this.ProcessLines(ref linesToProcess, data);
            }
            catch
            {
                // Logging done higher up
                return;
            }
        }

        private void ProcessLines(ref List<SectorData> lines, AbstractSectorDataFile file)
        {
            if (lines.Count == 0)
            {
                return;
            }

            if (this.IsNewCircleSectorlineDeclaration(lines[0]))
            {
                this.ParseCircleSectorline(ref lines, file);
            } else
            {
                this.ParseStandardSectorline(ref lines, file);
            }
        }
        
        /*
         * Returns true if it's a new declaration
         */
        private bool IsNewDeclaration(SectorData line)
        {
            return this.IsNewSectorlineDeclaration(line) || this.IsNewCircleSectorlineDeclaration(line);
        }

        /*
         * Returns true if it's a new CIRCLE_SECTORLINE declaration
         */
        private bool IsNewCircleSectorlineDeclaration(SectorData line)
        {
            return line.dataSegments[0] == "CIRCLE_SECTORLINE";
        }

        /*
         * Returns true if it's a SECTORLINE new declaration
         */
        private bool IsNewSectorlineDeclaration(SectorData line)
        {
            return line.dataSegments[0] == "SECTORLINE";
        }

        private void ParseCircleSectorline(ref List<SectorData> lines, AbstractSectorDataFile file)
        {
            // Deal with the declaration line
            SectorData declarationLine = lines[0];

            if (declarationLine.dataSegments.Count != 4 && declarationLine.dataSegments.Count != 5)
            {
                this.errorLog.AddEvent(
                    new SyntaxError("Incorrect number of segments for SECTORLINE declaration", declarationLine)
                );
                throw new ArgumentException();
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
                        new SyntaxError("Invalid CIRCLE_SECTORLINE coordinate", declarationLine)
                    );
                    throw new ArgumentException();
                }
            }

            if (
                double.TryParse(
                    declarationLine.dataSegments.Count == 4 ? declarationLine.dataSegments[3] : declarationLine.dataSegments[4],
                    out double radius) == false)
            {
                this.errorLog.AddEvent(
                    new SyntaxError("Invalid CIRCLE_SECTORLINE radius", declarationLine)
                );
                throw new ArgumentException();
            }


            List<SectorlineDisplayRule> displayRules = new List<SectorlineDisplayRule>();
            int i = 1;
            while (i < lines.Count)
            {
                SectorData displayData = lines[i];

                try
                {
                    displayRules.Add(this.ParseDisplayRule(displayData));
                } catch (ArgumentException exception) {
                    this.errorLog.AddEvent(
                        new SyntaxError(exception.Message, displayData)
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
                        declarationLine.definition,
                        declarationLine.docblock,
                        declarationLine.inlineComment
                    )
                );
            } else {
                this.sectorElements.Add(
                    new CircleSectorline(
                        declarationLine.dataSegments[1],
                        parsedCoordinate,
                        radius,
                        displayRules,
                        declarationLine.definition,
                        declarationLine.docblock,
                        declarationLine.inlineComment
                    )
                );
            }

        }

        private void ParseStandardSectorline(ref List<SectorData> lines, AbstractSectorDataFile file)
        {
            // Deal with the declaration line
            SectorData declarationLine = lines[0];

            if (declarationLine.dataSegments.Count != 2)
            {
                this.errorLog.AddEvent(
                    new SyntaxError("Incorrect number of segments for SECTORLINE declaration", declarationLine)
                );
                throw new ArgumentException();
            }

            List<SectorlineDisplayRule> displayRules = new List<SectorlineDisplayRule>();
            List<SectorlineCoordinate> coordinates = new List<SectorlineCoordinate>();
            int i = 1;
            while (i < lines.Count)
            {
                SectorData dataLine = lines[i];

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
                        this.errorLog.AddEvent(new SyntaxError("Invalid declaration in SECTORLINE declaration", dataLine));
                        throw new ArgumentException("Invalid declaration in SECTORLINE declaration");
                    }
                } catch (ArgumentException exception)
                {
                    this.errorLog.AddEvent(
                        new SyntaxError(exception.Message, dataLine)
                    );
                    throw exception;
                }

                i++;
            }

            if (coordinates.Count == 0)
            {
                this.errorLog.AddEvent(
                    new SyntaxError("No coordinates found for SECTORLINE", declarationLine)
                );
                throw new ArgumentException();
            }

            this.sectorElements.Add(
                new Sectorline(
                    declarationLine.dataSegments[1],
                    displayRules,
                    coordinates,
                    declarationLine.definition,
                    declarationLine.docblock,
                    declarationLine.inlineComment
                )
            );
        }

        private SectorlineDisplayRule ParseDisplayRule(SectorData data)
        {
            if (data.dataSegments.Count != 4)
            {
                this.errorLog.AddEvent(new SyntaxError("Invalid number of SECTORLINE DISPLAY rule segements", data));
                throw new ArgumentException();
            }

            return new SectorlineDisplayRule(
                data.dataSegments[1],
                data.dataSegments[2],
                data.dataSegments[3],
                data.definition,
                data.docblock,
                data.inlineComment
            );
        }

        private SectorlineCoordinate ParseCoordinate(SectorData data)
        {
            if (data.dataSegments.Count != 3)
            {
                this.errorLog.AddEvent(new SyntaxError("Invalid number of SECTORLINE COORD segements", data));
                throw new ArgumentException();
            }

            Coordinate parsedCoordinate = CoordinateParser.Parse(
                data.dataSegments[1],
                data.dataSegments[2]
            );

            if (parsedCoordinate.Equals(CoordinateParser.invalidCoordinate))
            {
                this.errorLog.AddEvent(new SyntaxError("Invalid SECTORLINE coordinate", data));
                throw new ArgumentException();
            }

            return new SectorlineCoordinate(
                parsedCoordinate,
                data.definition,
                data.docblock,
                data.inlineComment
            );
        }

        private bool IsDiplayDeclaration(SectorData data)
        {
            return data.dataSegments.Count > 0 && data.dataSegments[0] == "DISPLAY";
        }

        private bool IsCoordDeclaration(SectorData data)
        {
            return data.dataSegments.Count > 0 && data.dataSegments[0] == "COORD";
        }
    }
}
