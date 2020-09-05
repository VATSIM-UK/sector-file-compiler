using Compiler.Error;
using Compiler.Event;
using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Input;

namespace Compiler.Parser
{
    public class AirspaceParser : AbstractEseAirspaceParser, ISectorDataParser
    {
        private readonly SectorParser sectorParser;
        private readonly SectorlineParser sectorlineParser;
        private readonly CoordinationPointParser coordinationPointParser;
        private readonly ISectorLineParser sectorDataParser;
        private readonly IEventLogger eventLogger;

        public AirspaceParser(
            MetadataParser metadataParser,
            SectorParser sectorParser,
            SectorlineParser sectorlineParser,
            CoordinationPointParser coordinationPointParser,
            ISectorLineParser sectorDataParser,
            IEventLogger eventLogger
        )
            : base(metadataParser)
        {
            this.sectorParser = sectorParser;
            this.sectorlineParser = sectorlineParser;
            this.coordinationPointParser = coordinationPointParser;
            this.sectorDataParser = sectorDataParser;
            this.eventLogger = eventLogger;
        }

        public void ParseData(AbstractSectorDataFile data)
        {
            bool foundFirst = false;
            List<(int, string)> linesToProcess = new List<(int, string)>();
            foreach(string line in data)
            {
                if (this.ParseMetadata(line))
                {
                    continue;
                }

                // If we haven't found our first declaration, we expect one
                SectorFormatLine parsedLine = this.sectorDataParser.ParseLine(line);
                if (!foundFirst && !IsNewDeclaration(parsedLine))
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid type declaration in AIRSPACE section", data.FullPath, data.CurrentLineNumber)
                    );
                    return;
                }

                // Found our first declaration
                if (!foundFirst && IsNewDeclaration(parsedLine))
                {
                    foundFirst = true;
                    linesToProcess.Add((data.CurrentLineNumber, line));
                    continue;
                }

                // Found a new declaration, so send all the processed lines to the right sub-parser
                if (IsNewDeclaration(parsedLine))
                {
                    try
                    {
                        this.ProcessLines(linesToProcess, data.FullPath);
                    }
                    catch
                    {
                        // Do nothing, logged by individual parsers
                        return;
                    }
                    linesToProcess.Clear();
                }

                // Add the line to our lines to be processed.
                linesToProcess.Add((data.CurrentLineNumber, line));
            }

            if (linesToProcess.Count != 0)
            {
                // When we run out of lines to process do one last check
                try
                {
                    this.ProcessLines(linesToProcess, data.FullPath);
                }
                catch
                {
                    // Do nothing, logged by individual parsers
                    return;
                }
            }
        }

        private void ProcessLines(List<(int, string)> lines, string filename)
        {
            SectorFormatLine parsedLine = this.sectorDataParser.ParseLine(lines[0].Item2);
            switch (parsedLine.dataSegments[0])
            {
                case "COPX":
                case "FIR_COPX":
                    this.coordinationPointParser.ParseData(lines, filename);
                    break;
                case "SECTORLINE":
                case "CIRCLE_SECTORLINE":
                    this.sectorlineParser.ParseData(lines, filename);
                    break;
                case "SECTOR":
                    this.sectorParser.ParseData(lines, filename);
                    break;
            }
        }
    }
}
