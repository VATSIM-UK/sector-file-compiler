using System;
using Compiler.Parser;


namespace Compiler.Input
{
    public class InputLineReader
    {
        public static SectorFormatData ReadInputLines(IFileInterface file)
        {
            if (!file.Exists())
            {
                throw new ArgumentException("Input file not found " + file.GetPath());
            }

            return new SectorFormatData(
                file.GetPath(),
                file.GetNameWithoutExtension(),
                file.ParentFolder(),
                file.GetAllLines()
            );
        }
    }
}
