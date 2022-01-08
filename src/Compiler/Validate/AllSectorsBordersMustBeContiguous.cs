using System.Collections.Generic;
using Compiler.Event;
using Compiler.Model;
using Compiler.Error;
using Compiler.Argument;
using System.Linq;

namespace Compiler.Validate
{
    public class AllSectorBordersMustBeContiguous : IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events)
        {
            Dictionary<string, (Coordinate, Coordinate)> sectorlines = sectorElements.SectorLines.ToDictionary(
                sectorline => sectorline.Name,
                sectorline => (sectorline.Start(), sectorline.End())
            );

            foreach (Sector sector in sectorElements.Sectors)
            {
                foreach (SectorBorder border in sector.Borders)
                {
                    // Get the start and end coordinates of all the SECTORLINE elements used in the border.
                    List<KeyValuePair<string, (Coordinate, Coordinate)>> borderLines = border.BorderLines
                        .Where(borderLine => sectorlines.ContainsKey(borderLine))
                        .Select(
                            borderLine =>
                                new KeyValuePair<string, (Coordinate, Coordinate)>(borderLine, sectorlines[borderLine])
                        ).ToList();

                    if (borderLines.Count == 0)
                    {
                        continue;
                    }
                    
                    /*
                     * First up, lets work out whether it's the "first" or the "last" coordinate of the first BORDER
                     * segment that's joining up to the last BORDER segment, so we know how to proceed.
                     *
                     * If the END coordinate of the first border line matches up with any of the coordinates
                     * of the last border line, then it's going to be the START coordinate of that line that
                     * matches up with something on the second border line as we work our way around.
                     */
                    bool startOfLine = borderLines.First().Value.Item2.Equals(borderLines.Last().Value.Item1) ||
                                       borderLines.First().Value.Item2.Equals(borderLines.Last().Value.Item2);
                    
                    // Loop around the lines
                    for (int i = 0; i < borderLines.Count; i++)
                    {
                        var firstCoordinate = startOfLine
                            ? borderLines[i].Value.Item1
                            : borderLines[i].Value.Item2;

                        if (firstCoordinate.Equals(borderLines[(i + 1) % borderLines.Count].Value.Item1))
                        {
                            startOfLine = false;
                        }
                        else if (firstCoordinate.Equals(borderLines[(i + 1) % borderLines.Count].Value.Item2))
                        {
                            startOfLine = true;
                        }
                        else
                        {
                            events.AddEvent(new ValidationRuleFailure(
                                ErrorMessage(sector, borderLines[i].Key, borderLines[(i + 1) % borderLines.Count].Key),
                                border));
                            break;
                        }
                    }
                }
            }
        }

        private string ErrorMessage(Sector sector, string firstBorderline, string secondBorderline)
        {
            return
                $"Non-contiguous BORDER for SECTOR {sector.Name}, line {firstBorderline} does not join with {secondBorderline}";
        }
    }
}
