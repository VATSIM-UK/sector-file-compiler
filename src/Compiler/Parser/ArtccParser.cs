using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Model;
using Compiler.Event;
using Compiler.Error;
using Compiler.Input;

namespace Compiler.Parser
{
    public class ArtccParser : AbstractSectorElementParser, ISectorDataParser
    {
        private readonly ISectorLineParser sectorLineParser;
        private readonly ArtccType artccType;
        private readonly SectorElementCollection elements;
        private readonly IEventLogger eventLogger;

        public ArtccParser(
            MetadataParser metadataParser,
            ISectorLineParser sectorLineParser,
            ArtccType artccType,
            SectorElementCollection elements,
            IEventLogger eventLogger
        ) : base(metadataParser)
        {
            this.sectorLineParser = sectorLineParser;
            this.artccType = artccType;
            this.elements = elements;
            this.eventLogger = eventLogger;
        }

        public void ParseData(AbstractSectorDataFile data)
        {
            foreach (string line in data)
            {
                // Defer all metadata lines to the base
                if (this.ParseMetadata(line))
                {
                    continue;
                }

                SectorFormatLine sectorData = this.sectorLineParser.ParseLine(line);
                if (sectorData.dataSegments.Count < 5)
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Incorrect number of ARTCC segments", data.FullPath, data.CurrentLineNumber)
                    );
                    continue;
                }

                int count = sectorData.dataSegments.Count;

                // The points are at the end, so work backwards
                Point endPoint = PointParser.Parse(sectorData.dataSegments[count - 2], sectorData.dataSegments[count - 1]);
                if (endPoint.Equals(PointParser.invalidPoint))
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid ARTCC end point format: " + data.CurrentLine, data.FullPath, data.CurrentLineNumber)
                    );
                    return;
                }

                Point startPoint = PointParser.Parse(sectorData.dataSegments[count - 4], sectorData.dataSegments[count - 3]);
                if (startPoint.Equals(PointParser.invalidPoint))
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid ARTCC start point format: " + data.CurrentLine, data.FullPath, data.CurrentLineNumber)
                    );
                    return;
                }

                // Add it
                this.elements.Add(
                    new Artcc(
                        string.Join(" ", sectorData.dataSegments.GetRange(0, count - 4)),
                        this.artccType,
                        startPoint,
                        endPoint,
                        sectorData.comment
                    )
                );
            }
        }
    }
}
