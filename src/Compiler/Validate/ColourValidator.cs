using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Model;

namespace Compiler.Validate
{
    public class ColourValidator
    {
        public static bool ColourValid(SectorElementCollection sectorElements, string colour)
        {
            return IsValidColourInteger(colour) || ColourIsDefined(sectorElements, colour);
        }

        public static bool IsValidColourInteger(string colour)
        {
            return (int.TryParse(colour, out int colourValue) && colourValue >= 0 && colourValue <= 16777215);
        }

        public static bool ColourIsDefined(SectorElementCollection sectorElements, string colourString)
        {
            foreach (Colour colour in sectorElements.Colours)
            {
                if (colourString == colour.Name)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
