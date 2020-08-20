using System.Collections.Generic;
using Compiler.Model;
using Compiler.Error;
using Compiler.Event;
using Compiler.Validate;
using System;

namespace Compiler.Parser
{
    public class SectorParser : AbstractEseAirspaceParser
    {
        private readonly ISectorLineParser sectorLineParser;
        private readonly SectorElementCollection sectorElements;
        private readonly IEventLogger errorLog;

        public SectorParser(
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

        public int ParseData(SectorFormatData data, int i)
        {
            // Check the declaration
            SectorFormatLine declarationLine = this.sectorLineParser.ParseLine(data.lines[i]);
            if (declarationLine.dataSegments[0] != "SECTOR")
            {
                this.errorLog.AddEvent(
                    new SyntaxError("Invalid SECTOR declaration ", data.fullPath, i + 1)
                );
                throw new Exception();
            }

            // Check the minimum and maximum altitudes
            if (!int.TryParse(declarationLine.dataSegments[2], out int minimumAltitude))
            {
                this.errorLog.AddEvent(
                    new SyntaxError("SECTOR minimum altitude must be an integer ", data.fullPath, i + 1)
                );
                throw new Exception();
            }

            if (!int.TryParse(declarationLine.dataSegments[3], out int maximumAltitude))
            {
                this.errorLog.AddEvent(
                    new SyntaxError("SECTOR maximum altitude must be an integer ", data.fullPath, i + 1)
                );
                throw new Exception();
            }


            int nextLine = i + 1;
            SectorOwnerHierarchy ownerHierarchy = null;
            List<SectorAlternateOwnerHierarchy> altOwners = new List<SectorAlternateOwnerHierarchy>();
            SectorBorder border = new SectorBorder();
            List<SectorActive> actives = new List<SectorActive>();
            List<SectorGuest> guests = new List<SectorGuest>();
            SectorDepartureAirports departureAirports = new SectorDepartureAirports();
            SectorArrivalAirports arrivalAirports = new SectorArrivalAirports();
            while (nextLine < data.lines.Count)
            {
                // Defer all metadata lines to the base
                if (this.ParseMetadata(data.lines[nextLine]))
                {
                    nextLine++;
                    continue;
                }

                SectorFormatLine lineToParse = this.sectorLineParser.ParseLine(data.lines[nextLine]);

                if (IsNewDeclaration(lineToParse))
                {
                    break;
                }

                /*
                 * Parse each line one at a time, stopping if we reach a new declaration.
                 */
                try
                {
                    switch (lineToParse.dataSegments[0])
                    {
                        case "OWNER":
                            ownerHierarchy = this.ParseOwnerHierarchy(lineToParse);
                            break;
                        case "ALTOWNER":
                            altOwners.Add(this.ParseAlternateOwnerHierarchy(lineToParse));
                            break;
                        case "BORDER":
                            if (border.BorderLines.Count != 0)
                            {
                                throw new Exception("Each SECTOR declaration may only have one BORDER ");
                            }

                            border = this.ParseBorder(lineToParse);
                            break;
                        case "ACTIVE":
                            actives.Add(this.ParseActive(lineToParse));
                            break;
                        case "GUEST":
                            guests.Add(this.ParseGuest(lineToParse));
                            break;
                        case "DEPAPT":
                            if (departureAirports.Airports.Count != 0)
                            {
                                throw new Exception("Each SECTOR declaration may only have one DEPAPT definition ");
                            }

                            departureAirports = this.ParseDepartureAirport(lineToParse);
                            break;
                        case "ARRAPT":

                            if (arrivalAirports.Airports.Count != 0)
                            {
                                throw new Exception("Each SECTOR declaration may only have one ARRAPT definition ");
                            }

                            arrivalAirports = this.ParseArrivalAirport(lineToParse);
                            break;

                    }
                }
                catch (Exception exception)
                {
                    this.errorLog.AddEvent(
                        new SyntaxError(exception.Message, data.fullPath, nextLine + 1)
                    );
                    throw exception;
                }

                nextLine++;
            }

            if (ownerHierarchy == null)
            {
                this.errorLog.AddEvent(
                    new SyntaxError("Every SECTOR must have an owner ", data.fullPath, i + 1)
                );
                this.errorLog.AddEvent(
                    new ParserSuggestion("Have you added an OWNER declaration?")
                );
                throw new Exception();
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
                    border,
                    arrivalAirports,
                    departureAirports,
                    declarationLine.comment
                )
            );

            return nextLine - i;
        }

        /*
         * Parse and validate an OWNER line
         */
        private SectorOwnerHierarchy ParseOwnerHierarchy(SectorFormatLine line)
        {
            if (line.dataSegments.Count < 2)
            {
                throw new Exception("Invalid number of OWNER segements");
            }

            List<string> owners = new List<string>();

            for (int i = 1; i < line.dataSegments.Count; i++)
            {
                owners.Add(line.dataSegments[i]);
            }

            return new SectorOwnerHierarchy(
                owners,
                line.comment
            );
        }

        /*
         * Parse and valdiate an ALTOWNER line
         */
        private SectorAlternateOwnerHierarchy ParseAlternateOwnerHierarchy(SectorFormatLine line)
        {
            if (line.dataSegments.Count < 3)
            {
                throw new Exception("Invalid number of ALTOWNER segements");
            }

            List<string> altOwners = new List<string>();
            for (int i = 2; i < line.dataSegments.Count; i++)
            {
                altOwners.Add(line.dataSegments[i]);
            }

            return new SectorAlternateOwnerHierarchy(
                line.dataSegments[1],
                altOwners,
                line.comment
            );
        }

        /*
         * Parse and valdiate a BORDER line
         */
        private SectorBorder ParseBorder(SectorFormatLine line)
        {
            if (line.dataSegments.Count < 2)
            {
                throw new Exception("Invalid number of BORDER segements");
            }

            List<string> borders = new List<string>();
            for (int i = 1; i < line.dataSegments.Count; i++)
            {
                borders.Add(line.dataSegments[i]);
            }

            return new SectorBorder(
                borders,
                line.comment
            );
        }

        /*
         * Parse and valdiate a ACTIVE line
         */
        private SectorActive ParseActive(SectorFormatLine line)
        {
            if (line.dataSegments.Count != 3)
            {
                throw new Exception("Invalid number of ACTIVE segements ");
            }

            if (!AirportValidator.ValidEuroscopeAirport(line.dataSegments[1]))
            {
                throw new Exception("Invalid airport designator in ACTIVE segement ");
            }

            if (!RunwayValidator.RunwayValidIncludingAdjacent(line.dataSegments[2]))
            {
                throw new Exception("Invalid runway designator in ACTIVE segement ");
            }

            return new SectorActive(
                line.dataSegments[1],
                line.dataSegments[2],
                line.comment
            );
        }

        /*
        * Parse and valdiate a GUEST line
        */
        private SectorGuest ParseGuest(SectorFormatLine line)
        {
            if (line.dataSegments.Count != 4)
            {
                throw new Exception("Invalid number of GUEST segements ");
            }

            if (!AirportValidator.ValidSectorGuestAirport(line.dataSegments[2]))
            {
                throw new Exception("Invalid departure airport designator in GUEST segement ");
            }

            if (!AirportValidator.ValidSectorGuestAirport(line.dataSegments[3]))
            {
                throw new Exception("Invalid arrival airport designator in GUEST segement ");
            }

            return new SectorGuest(
                line.dataSegments[1],
                line.dataSegments[2],
                line.dataSegments[3],
                line.comment
            );
        }

        /*
        * Parse and valdiate a DEPAPT line
        */
        private SectorDepartureAirports ParseDepartureAirport(SectorFormatLine line)
        {
            if (line.dataSegments.Count < 2)
            {
                throw new Exception("Invalid number of DEPAPT segments ");
            }

            List<string> airports = new List<string>();
            for (int i = 1; i < line.dataSegments.Count; i++)
            {
                if (!AirportValidator.IcaoValid(line.dataSegments[i]))
                {
                    throw new Exception("Invalid ICAO code in DEPAPT ");
                }

                airports.Add(line.dataSegments[i]);
            }

            return new SectorDepartureAirports(
                airports,
                line.comment
            );
        }

        /*
        * Parse and valdiate a DEPAPT line
        */
        private SectorArrivalAirports ParseArrivalAirport(SectorFormatLine line)
        {
            if (line.dataSegments.Count < 2)
            {
                throw new Exception("Invalid number of ARRAPT segments ");
            }

            List<string> airports = new List<string>();
            for (int i = 1; i < line.dataSegments.Count; i++)
            {
                if (!AirportValidator.IcaoValid(line.dataSegments[i]))
                {
                    throw new Exception("Invalid ICAO code in ARRAPT ");
                }

                airports.Add(line.dataSegments[i]);
            }

            return new SectorArrivalAirports(
                airports,
                line.comment
            );
        }
    }
}
