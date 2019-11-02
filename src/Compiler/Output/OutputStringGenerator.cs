using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Output
{
    public class OutputStringGenerator
    {
        public static string GenerateOutput(List<string> lines)
        {
            return String.Join("\r\n", lines) + "\r\n";
        }
    }
}
