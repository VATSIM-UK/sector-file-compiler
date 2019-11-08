using System;
using Compiler.Parser;


namespace Compiler.Input
{
    public class InputLineReader
    {
        public SectorFormatData ReadInputLines(InputFile file)
        {
            if (!file.Exists())
            {
                throw new ArgumentException("Input file not found " + file.GetPath());
            }

            return new SectorFormatData(
                file.GetPath(),
                file.GetAllLines()
            );
        }
    }
}
