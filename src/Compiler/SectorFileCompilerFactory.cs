using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Compiler.Argument;
using Compiler.Output;

namespace Compiler
{
    public class SectorFileCompilerFactory
    {
        public static SectorFileCompiler Create(CompilerArguments arguments, IOutputInterface output)
        {
            return new SectorFileCompiler(
                arguments,
                new Logger(output, arguments)
            );
        }
    }
}
