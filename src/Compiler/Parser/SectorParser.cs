using System.Collections.Generic;
using Compiler.Model;
using Compiler.Error;
using Compiler.Input;
using Compiler.Event;
using Compiler.Validate;
using System;

namespace Compiler.Parser
{
    public class SectorParser : ISectorDataParser
    {
        private readonly SectorElementCollection sectorElements;
        private readonly IEventLogger errorLog;

        public SectorParser(
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
                    this.errorLog.AddEvent(
                        new SyntaxError("Invalid SECTOR declaration", line)
                    );
                    return;
                }

                if (!foundFirst)
                {
                    linesToProcess.Add(line);
                    foundFirst = true;
                    continue;
                }

                if (this.IsNewDeclaration(line))
                {
                    this.ProcessLines(ref linesToProcess, data);
                    linesToProcess.Clear();
                }

                linesToProcess.Add(line);
            }

            this.ProcessLines(ref linesToProcess, data);
        }

        public void ProcessLines(ref List<SectorData> lines, AbstractSectorDataFile file)
        {
            if (lines.Count == 0)
            {
                return;
            }

            SectorData declarationLine = lines[0];

            int minimumAltitude = 0;
            int maximumAltitude = 0;
            if (declarationLine.dataSegments[0] != "SECTOR")
            {
                this.errorLog.AddEvent(
                    new SyntaxError("Invalid SECTOR declaration", declarationLine)
                );
                return;
            }

            // Check the minimum and maximum altitudes
            if (!int.TryParse(declarationLine.dataSegments[2], out minimumAltitude))
            {
                this.errorLog.AddEvent(
                    new SyntaxError("SECTOR minimum altitude must be an integer", declarationLine)
                );
                return;
            }

            if (!int.TryParse(declarationLine.dataSegments[3], out maximumAltitude))
            {
                this.errorLog.AddEvent(
                    new SyntaxError("SECTOR maximum altitude must be an integer", declarationLine)
                );
                return;
            }

            SectorOwnerHierarchy ownerHierarchy = null;
            List<SectorAlternateOwnerHierarchy> altOwners = new List<SectorAlternateOwnerHierarchy>();
            List<SectorBorder> borders = new List<SectorBorder>();
            List<SectorActive> actives = new List<SectorActive>();
            List<SectorGuest> guests = new List<SectorGuest>();
            List<SectorDepartureAirports> departureAirports = new List<SectorDepartureAirports>();
            List<SectorArrivalAirports> arrivalAirports = new List<SectorArrivalAirports>();

            for (int i = 1; i < lines.Count; i++)
            {
                try
                {
                    switch (lines[i].dataSegments[0])
                    {
                        case "OWNER":
                            ownerHierarchy = this.ParseOwnerHierarchy(lines[i]);
                            break;
                        case "ALTOWNER":
                            altOwners.Add(this.ParseAlternateOwnerHierarchy(lines[i]));
                            break;
                        case "BORDER":
                            borders.Add(this.ParseBorder(lines[i]));
                            break;
                        case "ACTIVE":
                            actives.Add(this.ParseActive(lines[i]));
                            break;
                        case "GUEST":
                            guests.Add(this.ParseGuest(lines[i]));
                            break;
                        case "DEPAPT":
                            departureAirports.Add(this.ParseDepartureAirport(lines[i]));
                            break;
                        case "ARRAPT":
                            arrivalAirports.Add(this.ParseArrivalAirport(lines[i]));
                            break;
                        default:
                            this.errorLog.AddEvent(
                                 new SyntaxError("Unknown SECTOR line type", lines[i])
                            );
                            return;
                    }
                }
                catch (ArgumentException exception)
                {
                    this.errorLog.AddEvent(
                        new SyntaxError(exception.Message, lines[i])
                    );
                    return;
                }

            }
        
            if (ownerHierarchy == null)
            {
                this.errorLog.AddEvent(
                    new SyntaxError("Every SECTOR must have an owner", declarationLine)
                );
                this.errorLog.AddEvent(
                    new ParserSuggestion("Have you added an OWNER declaration?")
                );
                return;
            }

            this.sectorElements.Add(
                new Sector(
                    declarationLine.dataSegments[1],
                    minimumAltitude,
                    maximumAltitude,
                    ownerHierarchy,
                    altOwners,
                    actives,
                    guests,
                    borders,
                    arrivalAirports,
                    departureAirports,
                    declarationLine.definition,
                    declarationLine.docblock,
                    declarationLine.inlineComment
                )
            );
        }

        public bool IsNewDeclaration(SectorData line)
        {
            return line.dataSegments[0] == "SECTOR";
        }

        /*
         * Parse and validate an OWNER line
         */
        private SectorOwnerHierarchy ParseOwnerHierarchy(SectorData line)
        {
            if (line.dataSegments.Count < 2)
            {
                throw new ArgumentException("Invalid number of OWNER segements");
            }

            List<string> owners = new List<string>();

            for (int i = 1; i < line.dataSegments.Count; i++)
            {
                owners.Add(line.dataSegments[i]);
            }

            return new SectorOwnerHierarchy(
                owners,
                line.definition,
                line.docblock,
                line.inlineComment
            );
        }

        /*
         * Parse and valdiate an ALTOWNER line
         */
        private SectorAlternateOwnerHierarchy ParseAlternateOwnerHierarchy(SectorData line)
        {
            if (line.dataSegments.Count < 3)
            {
                throw new ArgumentException("Invalid number of ALTOWNER segements");
            }

            List<string> altOwners = new List<string>();
            for (int i = 2; i < line.dataSegments.Count; i++)
            {
                altOwners.Add(line.dataSegments[i]);
            }

            return new SectorAlternateOwnerHierarchy(
                line.dataSegments[1],
                altOwners,
                line.definition,
                line.docblock,
                line.inlineComment
            );
        }

        /*
         * Parse and valdiate a BORDER line
         */
        private SectorBorder ParseBorder(SectorData line)
        {
            if (line.dataSegments.Count < 2)
            {
                throw new ArgumentException("Invalid number of BORDER segements");
            }

            List<string> borders = new List<string>();
            for (int i = 1; i < line.dataSegments.Count; i++)
            {
                borders.Add(line.dataSegments[i]);
            }

            return new SectorBorder(
                borders,
                line.definition,
                line.docblock,
                line.inlineComment
            );
        }

        /*
         * Parse and valdiate a ACTIVE line
         */
        private SectorActive ParseActive(SectorData line)
        {
            if (line.dataSegments.Count != 3)
            {
                throw new ArgumentException("Invalid number of ACTIVE segements ");
            }

            if (!AirportValidator.ValidEuroscopeAirport(line.dataSegments[1]))
            {
                throw new ArgumentException("Invalid airport designator in ACTIVE segement ");
            }

            if (!RunwayValidator.RunwayValidIncludingAdjacent(line.dataSegments[2]))
            {
                throw new ArgumentException("Invalid runway designator in ACTIVE segement ");
            }

            return new SectorActive(
                line.dataSegments[1],
                line.dataSegments[2],
                line.definition,
                line.docblock,
                line.inlineComment
            );
        }

        /*
        * Parse and valdiate a GUEST line
        */
        private SectorGuest ParseGuest(SectorData line)
        {
            if (line.dataSegments.Count != 4)
            {
                throw new ArgumentException("Invalid number of GUEST segements ");
            }

            if (!AirportValidator.ValidSectorGuestAirport(line.dataSegments[2]))
            {
                throw new ArgumentOutOfRangeException("Invalid departure airport designator in GUEST segement");
            }

            if (!AirportValidator.ValidSectorGuestAirport(line.dataSegments[3]))
            {
                throw new ArgumentException("Invalid arrival airport designator in GUEST segement ");
            }

            return new SectorGuest(
                line.dataSegments[1],
                line.dataSegments[2],
                line.dataSegments[3],
                line.definition,
                line.docblock,
                line.inlineComment
            );
        }

        /*
        * Parse and valdiate a DEPAPT line
        */
        private SectorDepartureAirports ParseDepartureAirport(SectorData line)
        {
            if (line.dataSegments.Count < 2)
            {
                throw new ArgumentException("Invalid number of DEPAPT segments ");
            }

            List<string> airports = new List<string>();
            for (int i = 1; i < line.dataSegments.Count; i++)
            {
                if (!AirportValidator.IcaoValid(line.dataSegments[i]))
                {
                    throw new ArgumentException("Invalid ICAO code in DEPAPT ");
                }

                airports.Add(line.dataSegments[i]);
            }

            return new SectorDepartureAirports(
                airports,
                line.definition,
                line.docblock,
                line.inlineComment
            );
        }

        /*
        * Parse and valdiate a DEPAPT line
        */
        private SectorArrivalAirports ParseArrivalAirport(SectorData line)
        {
            if (line.dataSegments.Count < 2)
            {
                throw new ArgumentException("Invalid number of ARRAPT segments ");
            }

            List<string> airports = new List<string>();
            for (int i = 1; i < line.dataSegments.Count; i++)
            {
                if (!AirportValidator.IcaoValid(line.dataSegments[i]))
                {
                    throw new ArgumentException("Invalid ICAO code in ARRAPT ");
                }

                airports.Add(line.dataSegments[i]);
            }

            return new SectorArrivalAirports(
                airports,
                line.definition,
                line.docblock,
                line.inlineComment
            );
        }
    }
}
