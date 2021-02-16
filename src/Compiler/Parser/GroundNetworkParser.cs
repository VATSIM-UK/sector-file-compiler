using System;
using System.Collections.Generic;
using System.Linq;
using Compiler.Error;
using Compiler.Event;
using Compiler.Input;
using Compiler.Model;
using Compiler.Validate;

namespace Compiler.Parser
{
    public class GroundNetworkParser: ISectorDataParser
    {
        private const string TaxiDeclaration = "TAXI";
        private const string ExitDeclaration = "EXIT";
        private SectorElementCollection sectorElements;
        private IEventLogger errorLog;

        public GroundNetworkParser(
            SectorElementCollection sectorElements,
            IEventLogger errorLog
        ) {
            this.sectorElements = sectorElements;
            this.errorLog = errorLog;
        }

        public void ParseData(AbstractSectorDataFile data)
        {
            bool doneFirstLine = false;
            List<SectorData> lines = new();
            List<GroundNetworkTaxiway> taxiways = new();
            List<GroundNetworkRunwayExit> exits = new();
            foreach (SectorData line in data)
            {
                if (!doneFirstLine)
                {
                    lines.Add(line);
                    doneFirstLine = true;
                } 
                else if (IsNewDeclaration(line))
                {
                    try
                    {
                        ProcessLines(data, lines, taxiways, exits);
                    }
                    catch
                    {
                        return;
                    }
                    lines.Clear();
                    lines.Add(line);
                } else
                {
                    lines.Add(line);
                }
            }

            if (lines.Any())
            {
                try
                {
                    ProcessLines(data, lines, taxiways, exits);
                }
                catch
                {
                    return;
                }
            }
            
            sectorElements.Add(
                new GroundNetwork(
                    data.GetParentDirectoryName(),
                    taxiways,
                    exits
                )
            );
        }

        private void ProcessLines(
            AbstractSectorDataFile file,
            List<SectorData> lines,
            List<GroundNetworkTaxiway> taxiways,
            List<GroundNetworkRunwayExit> exits
        ) {
            switch (lines[0].dataSegments[0])
            {
                case TaxiDeclaration:
                    taxiways.Add(ProcessTaxiway(file, lines));
                    break;
                case ExitDeclaration:
                    exits.Add(ProcessExit(file, lines));
                    break;
                default:
                    errorLog.AddEvent(new SyntaxError("Unknown ground network declaration type", lines[0]));
                    throw new ArgumentException();
            }
        }

        private GroundNetworkTaxiway ProcessTaxiway(AbstractSectorDataFile file, List<SectorData> lines)
        {
            SectorData declarationLine = lines[0];
            if (declarationLine.dataSegments.Count < 3 || declarationLine.dataSegments.Count > 5)
            {
                errorLog.AddEvent(
                    new SyntaxError("Invalid number of TAXI declaration segments", declarationLine)
                );
                throw new ArgumentException();
            }
            
            if (
                !int.TryParse(declarationLine.dataSegments[2], out int maximumSpeed) ||
                maximumSpeed < 0
            ) {
                errorLog.AddEvent(
                    new SyntaxError("Invalid maximum speed in TAXI declaration", declarationLine)
                );
                throw new ArgumentException();
            }


            int? usageFlag = null;
            if (
                declarationLine.dataSegments.Count >= 4 &&
                declarationLine.dataSegments[3] != ""
            ) {

                if (
                    !int.TryParse(declarationLine.dataSegments[3], out int parsedUsageFlag) ||
                    parsedUsageFlag < 1 ||
                    parsedUsageFlag > 3
                ) {
                    errorLog.AddEvent(
                        new SyntaxError("Invalid usage flag in TAXI declaration", declarationLine)
                    );
                    throw new ArgumentException();
                }

                usageFlag = parsedUsageFlag;
            }

            string gateName = declarationLine.dataSegments.Count == 5
                ? (declarationLine.dataSegments[4] == "" ? null : declarationLine.dataSegments[4])
                : null;

            return new GroundNetworkTaxiway(
                declarationLine.dataSegments[1],
                maximumSpeed,
                usageFlag,
                gateName,
                ProcessCoordinates(file, lines.GetRange(1, lines.Count - 1), TaxiDeclaration),
                declarationLine.definition,
                declarationLine.docblock,
                declarationLine.inlineComment
            );
        }
        
        private GroundNetworkRunwayExit ProcessExit(AbstractSectorDataFile file, List<SectorData> lines)
        {
            SectorData declarationLine = lines[0];
            if (declarationLine.dataSegments.Count != 5)
            {
                errorLog.AddEvent(
                    new SyntaxError("Invalid number of EXIT declaration segments", declarationLine)
                );
                throw new ArgumentException();
            }

            if (!RunwayValidator.RunwayValid(declarationLine.dataSegments[1]))
            {
                errorLog.AddEvent(
                    new SyntaxError("Invalid runway in EXIT declaration", declarationLine)
                );
                throw new ArgumentException();
            }

            if (!HeadingParser.TryParse(declarationLine.dataSegments[3], out int direction))
            {
                errorLog.AddEvent(
                    new SyntaxError("Invalid exit direction in EXIT declaration", declarationLine)
                );
                throw new ArgumentException();
            }

            if (!int.TryParse(declarationLine.dataSegments[4], out int maximumSpeed))
            {
                errorLog.AddEvent(
                    new SyntaxError("Invalid maximum speed in EXIT declaration", declarationLine)
                );
                throw new ArgumentException();
            }

            return new GroundNetworkRunwayExit(
                declarationLine.dataSegments[1],
                declarationLine.dataSegments[2],
                direction,
                maximumSpeed,
                ProcessCoordinates(file, lines.GetRange(1, lines.Count - 1), ExitDeclaration),
                declarationLine.definition,
                declarationLine.docblock,
                declarationLine.inlineComment
            );
        }

        private List<GroundNetworkCoordinate> ProcessCoordinates(
            AbstractSectorDataFile file,
            List<SectorData> lines,
            string parentElementType
        ) {
            if (lines.Count == 0)
            {
                errorLog.AddEvent(
                    new SyntaxError(
                        $"All ground network {parentElementType} declarations must have at least one coordinate", 
                        file.GetFileName()
                    )
                );
                throw new ArgumentException();
            }

            List<GroundNetworkCoordinate> coordinates = new();
            for (int i = 0; i < lines.Count; i++)
            {
                SectorData line = lines[i];
                if (line.dataSegments.Count != 3)
                {
                    errorLog.AddEvent(
                        new SyntaxError($"Invalid number of {parentElementType} COORD segments", lines[i])
                    );
                    throw new ArgumentException();
                }
                
                if (line.dataSegments[0] != "COORD")
                {
                    errorLog.AddEvent(
                        new SyntaxError($"Invalid COORD in {parentElementType} declaration", lines[i])
                    );
                    throw new ArgumentException();
                }

                if (
                    !CoordinateParser.TryParse(
                        line.dataSegments[1],
                        line.dataSegments[2],
                        out Coordinate parsedCoordinate
                    )
                ) {
                    errorLog.AddEvent(
                        new SyntaxError($"Invalid coordinate in {parentElementType} declaration", lines[i])
                    );
                    throw new ArgumentException();
                }
                
                coordinates.Add(
                    new GroundNetworkCoordinate(
                        parsedCoordinate,
                        line.definition,
                        line.docblock,
                        line.inlineComment
                    )
                );
            }
            
            return coordinates;
        }

        private bool IsNewDeclaration(SectorData line)
        {
            return line.dataSegments[0] == TaxiDeclaration ||
                   line.dataSegments[0] == ExitDeclaration;
        }
    }
}