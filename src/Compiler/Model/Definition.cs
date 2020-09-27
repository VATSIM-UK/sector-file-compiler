using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Model
{
    /*
     * Represents where something was defined.
     */
    public class Definition
    {
        public Definition(string filename, int lineNumber)
        {
            Filename = filename;
            LineNumber = lineNumber;
        }

        public string Filename { get; }
        public int LineNumber { get; }
    }
}
