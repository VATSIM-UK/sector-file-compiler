using System;

namespace Compiler.Exception
{
    public class InputDirectoryNotFoundException: ArgumentException
    {
        public InputDirectoryNotFoundException(string directory)
            : base($"Input directory not found: {directory}")
        {

        }
    }
}
