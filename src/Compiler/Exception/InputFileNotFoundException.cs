using System;

namespace Compiler.Exception
{
    public class InputFileNotFoundException: ArgumentException
    {
        public InputFileNotFoundException(string filename)
            : base(string.Format("Input file not found: {0}", filename))
        {

        }
    }
}
