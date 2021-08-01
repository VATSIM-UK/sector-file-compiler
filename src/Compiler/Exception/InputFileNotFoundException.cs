using System;
using System.Diagnostics.CodeAnalysis;

namespace Compiler.Exception
{
    [ExcludeFromCodeCoverage]
    public class InputFileNotFoundException: ArgumentException
    {
        public InputFileNotFoundException(string filename)
            : base($"Input file not found: {filename}")
        {

        }
    }
}
