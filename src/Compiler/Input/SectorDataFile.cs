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
                    const double R = 6372.795477598;

                    System.Console.WriteLine(line);

                    Regex rx = new Regex(@";Arc region (.*) centre ([NS]\d{3}\.\d{2}\.\d{2}\.\d{3}) ([EW]\d{3}\.\d{2}\.\d{2}\.\d{3}) radius (\d*(?:\.\d*){0,1})(?: from ([NS]\d{3}\.\d{2}\.\d{2}\.\d{3}) ([EW]\d{3}\.\d{2}\.\d{2}\.\d{3}) to ([NS]\d{3}\.\d{2}\.\d{2}\.\d{3}) ([EW]\d{3}\.\d{2}\.\d{2}\.\d{3})){0,1}", RegexOptions.None);
                    GroupCollection groups = rx.Match(line).Groups;  // error catching!

                    string regionName = groups[1].Value;
                    System.Console.WriteLine(groups[1].Value);

                    double lat = Coordinate.DegreeMinSecToDecimalDegree(groups[2].Value);
                    double lon = Coordinate.DegreeMinSecToDecimalDegree(groups[3].Value);

                    float radius = float.Parse(groups[4].Value);

                    int initialTheta = 0;
                    int finalTheta = 360;

                    string prevLat = "";
                    string prevLon = "";

                    bool includesFromTo = false;

                    if (groups.Count > 5) {  // includes a from / to as well
                        includesFromTo = true;
                        prevLat = groups[5].Value;
                        prevLon = groups[6].Value;

                        double fromLat = Coordinate.DegreeMinSecToDecimalDegree(groups[5].Value);
                        double fromLon = Coordinate.DegreeMinSecToDecimalDegree(groups[6].Value);

                        double toLat = Coordinate.DegreeMinSecToDecimalDegree(groups[7].Value);
                        double toLon = Coordinate.DegreeMinSecToDecimalDegree(groups[8].Value);

                        // Calculate angle to from / to coordinate

                        double fromTheta = Math.Atan2(Math.Cos(fromLat * Math.PI / 180) * Math.Sin((fromLon - lon) * Math.PI / 180), 
                            Math.Cos(lat * Math.PI / 180) * Math.Sin(fromLat * Math.PI / 180) - Math.Sin(lat * Math.PI / 180) * Math.Cos(fromLat * Math.PI / 180) * Math.Cos((fromLon - lon) * Math.PI / 180)
                        );
                        fromTheta = (180 * fromTheta / Math.PI + 360) % 360;

                        double toTheta = Math.Atan2(Math.Cos(toLat * Math.PI / 180) * Math.Sin((toLon - lon) * Math.PI / 180),
                            Math.Cos(lat * Math.PI / 180) * Math.Sin(toLat * Math.PI / 180) - Math.Sin(lat * Math.PI / 180) * Math.Cos(toLat * Math.PI / 180) * Math.Cos((toLon - lon) * Math.PI / 180)
                        );
                        toTheta = (180 * toTheta / Math.PI + 360) % 360;

                        initialTheta = (int)Math.Min(fromTheta, toTheta);
                        finalTheta = (int)Math.Max(fromTheta, toTheta);

                        // calculate actual radius

                        double fromDist = R * Math.Acos(Math.Sin(lat * Math.PI / 180) * Math.Sin(fromLat * Math.PI / 180) + Math.Cos(lat * Math.PI / 180) * Math.Cos(fromLat * Math.PI / 180) * Math.Cos((lon - fromLon) * Math.PI / 180));
                        fromDist = fromDist / 1.852;
                        double toDist = R * Math.Acos(Math.Sin(lat * Math.PI / 180) * Math.Sin(toLat * Math.PI / 180) + Math.Cos(lat * Math.PI / 180) * Math.Cos(toLat * Math.PI / 180) * Math.Cos((lon - toLon) * Math.PI / 180));
                        toDist = toDist / 1.852;

                        float meanRadius = (float)Math.Round((fromDist + toDist) / 2, 2);

                        if (meanRadius != radius) {
                            System.Console.WriteLine("RADIUS ERROR REPLACE ME BEFORE RELEASE");
                            radius = meanRadius;
                        }

                        

                        //System.Console.WriteLine(fromTheta);
                        //System.Console.WriteLine(toTheta);
                        //throw new System.Exception();
                    }

                    for (int theta = initialTheta + 1; theta < finalTheta; theta += DELTA_THETA) {
                        double deltaLat = (radius * Math.Cos(theta * Math.PI / 180)) / 60.0d;
                        double deltaLon = (radius * Math.Sin(theta * Math.PI / 180)) / 60.0d;
                        deltaLon /= Math.Cos((lat) * Math.PI / 180); // account for length of nautical mile changing with latitude

                        string newLat = Coordinate.DecimalDegreeToDegreeMinSec(lat + deltaLat, true);
                        string newLon = Coordinate.DecimalDegreeToDegreeMinSec(lon + deltaLon, false);

                        if (theta == initialTheta + 1 && !includesFromTo) {
                            prevLat = newLat;
                            prevLon = newLon;
                            continue;
                        }

                        string outLine = $"{regionName} {prevLat} {prevLon} {newLat} {newLon}";

                        prevLat = newLat;
                        prevLon = newLon;

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

                    if (includesFromTo) {
                        string outLine = $"{regionName} {prevLat} {prevLon} {groups[7].Value} {groups[8].Value}";
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
