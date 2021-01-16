using System;

namespace Compiler.Exception
{
    public class InputFileNotFoundException: ArgumentException
    {
        public InputFileNotFoundException(string filename)
            : base($"Input file not found: {filename}")
        {

        }
    }
}
