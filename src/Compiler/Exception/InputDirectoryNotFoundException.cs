using System;
using System.Diagnostics.CodeAnalysis;

namespace Compiler.Exception
{
    [ExcludeFromCodeCoverage]
    public class InputDirectoryNotFoundException: ArgumentException
    {
        public InputDirectoryNotFoundException(string directory)
            : base($"Input directory not found: {directory}")
        {

        }
    }
}
