using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Model;
using Compiler.Event;
using Compiler.Error;
using Compiler.Input;

namespace Compiler.Parser
{
    public class ArtccParser : ISectorDataParser
    {
        private readonly ArtccType artccType;
        private readonly SectorElementCollection elements;
        private readonly IEventLogger eventLogger;

        public ArtccParser(
            ArtccType artccType,
            SectorElementCollection elements,
            IEventLogger eventLogger
        ) {
            this.artccType = artccType;
            this.elements = elements;
            this.eventLogger = eventLogger;
        }

        public void ParseData(AbstractSectorDataFile data)
        {
            foreach (SectorData line in data)
            {
                if (line.dataSegments.Count < 5)
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Incorrect number of ARTCC segments", line)
                    );
                    continue;
                }

                int count = line.dataSegments.Count;

                // The points are at the end, so work backwards
                Point endPoint = PointParser.Parse(line.dataSegments[count - 2], line.dataSegments[count - 1]);
                if (endPoint.Equals(PointParser.invalidPoint))
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid ARTCC end point format: " + data.CurrentLine, line)
                    );
                    return;
                }

                Point startPoint = PointParser.Parse(line.dataSegments[count - 4], line.dataSegments[count - 3]);
                if (startPoint.Equals(PointParser.invalidPoint))
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid ARTCC start point format: " + data.CurrentLine, line)
                    );
                    return;
                }

                // Add it
                this.elements.Add(
                    new ArtccSegment(
                        string.Join(" ", line.dataSegments.GetRange(0, count - 4)),
                        this.artccType,
                        startPoint,
                        endPoint,
                        line.definition,
                        line.docblock,
                        line.inlineComment
                    )
                );
            }
        }
    }
}
