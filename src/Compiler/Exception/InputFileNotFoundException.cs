using System;

namespace Compiler.Exception
{
    class InputFileNotFoundException: ArgumentException
    {
        public InputFileNotFoundException(string filename)
            : base(string.Format("Input file not found: {0}", filename))
        {

        }
    }
}
