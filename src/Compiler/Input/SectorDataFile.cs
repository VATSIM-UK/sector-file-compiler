using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Compiler.Model;

namespace Compiler.Input
{
    public class SectorDataFile: AbstractSectorDataFile
    {
        private readonly IInputStreamFactory streamFactory;
        private readonly AbstractSectorDataReader reader;

        public SectorDataFile(string fullPath, IInputStreamFactory streamFactory, InputDataType dataType, AbstractSectorDataReader reader)
            : base(fullPath, dataType)
        {
            this.streamFactory = streamFactory;
            this.reader = reader;
        }

        /*
         * Iterate the lines in a file.
         * - Skip any blank line
         * - Store up any full-comment lines to be turned into a DocBlock
         * - If it's a data line, yield a data item for parsing
         */
        public override IEnumerator<SectorData> GetEnumerator()
        {
            Docblock docblock = new Docblock();
            using TextReader file = this.streamFactory.GetStream(this.FullPath);
            string line;
            while ((line = file.ReadLine()) != null)
            {
                this.CurrentLineNumber++;
                if (reader.IsBlankLine(line))
                {
                    continue;
                }

                if (reader.IsCommentLine(line))
                {
                    docblock.AddLine(reader.GetCommentSegment(line));
                }
                else if (reader.IsArcGenLine(line)) {
                    // Return new SectorData for each new region

                    const int DELTA_THETA = 5;

                    System.Console.WriteLine(line);

                    Regex rx = new Regex(@";Arc region (.*) centre ([NS]\d{3}\.\d{2}\.\d{2}\.\d{3}) ([EW]\d{3}\.\d{2}\.\d{2}\.\d{3}) radius (\d*(?:\.\d*){0,1})(?: from ([NS]\d{3}\.\d{2}\.\d{2}\.\d{3}) ([EW]\d{3}\.\d{2}\.\d{2}\.\d{3}) to ([NS]\d{3}\.\d{2}\.\d{2}\.\d{3}) ([EW]\d{3}\.\d{2}\.\d{2}\.\d{3})){0,1}", RegexOptions.None);
                    GroupCollection groups = rx.Match(line).Groups;  // error catching!

                    string regionName = groups[1].Value;
                    System.Console.WriteLine(groups[1].Value);

                    double lat = Coordinate.DegreeMinSecToDecimalDegree(groups[2].Value);
                    double lon = Coordinate.DegreeMinSecToDecimalDegree(groups[3].Value);

                    float radius = float.Parse(groups[4].Value);

                    string prevLat = "";
                    string prevLon = "";

                    for (int theta = 0; theta <= 360; theta += DELTA_THETA) {
                        double deltaLat = (radius * Math.Cos(theta * Math.PI / 180)) / 60.0d;
                        double deltaLon = (radius * Math.Sin(theta * Math.PI / 180)) / 60.0d;
                        deltaLon /= Math.Cos((lat) * Math.PI / 180); // account for length of nautical mile changing with latitude

                        string newLat = Coordinate.DecimalDegreeToDegreeMinSec(lat + deltaLat, true);
                        string newLon = Coordinate.DecimalDegreeToDegreeMinSec(lon + deltaLon, false);

                        if (theta == 0) {
                            prevLat = newLat;
                            prevLon = newLon;
                            continue;
                        }

                        string outLine = $"{regionName} {prevLat} {prevLon} {newLat} {newLon}";

                        prevLat = newLat;
                        prevLon = newLon;
                        //System.Console.WriteLine(newLat + "," + newLon);

                        // For each new coordinate, yield return it.

                        yield return new SectorData(
                            docblock,
                            reader.GetCommentSegment(outLine),
                            reader.GetDataSegments(outLine),
                            reader.GetRawData(outLine),
                            new Definition(this.FullPath, this.CurrentLineNumber)  // sus
                        );
                        docblock = new Docblock();
                    }
                }
                else
                {
                    yield return new SectorData(
                        docblock,
                        reader.GetCommentSegment(line),
                        reader.GetDataSegments(line),
                        reader.GetRawData(line),
                        new Definition(this.FullPath, this.CurrentLineNumber)
                    );
                    docblock = new Docblock();
                }
            }
        }
    }
}
