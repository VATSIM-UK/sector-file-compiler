using System;
using System.Collections.Generic;
using Compiler.Parser;


namespace Compiler.Input
{
    public class InputLineReader
    {
        public static readonly SectorFormatData invalidData = new SectorFormatData(
            "",
            "",
            "",
            new List<string>()
        );

        public static SectorFormatData ReadInputLines(IFileInterface file)
        {
            if (!file.Exists())
            {
                return InputLineReader.invalidData;
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
