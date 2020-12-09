using System;

namespace Compiler.Exception
{
    class InputDirectoryNotFoundException: ArgumentException
    {
        public InputDirectoryNotFoundException(string directory)
            : base(string.Format("Input directory not found: {0}", directory))
        {

        }
    }
}
