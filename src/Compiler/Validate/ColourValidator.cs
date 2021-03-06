﻿using Compiler.Model;

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
            return int.TryParse(colour, out int colourValue) && ValidateColourInt(colourValue);
        }

        private static bool ValidateColourInt(int colourValue)
        {
            return colourValue >= 0 && colourValue <= 16777215;
        }

        public static bool ColourIsDefined(SectorElementCollection sectorElements, string colourString)
        {
            foreach (Colour colour in sectorElements.Colours)
            {
                if (colourString.ToLower() == colour.Name.ToLower())
                {
                    return true;
                }
            }

            return false;
        }
    }
}
