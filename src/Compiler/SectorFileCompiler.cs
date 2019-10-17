using Compiler.Argument;
using Compiler.Input;
using Compiler.Output;
using System.Collections.Generic;
using System.IO;

namespace Compiler
{
    public class SectorFileCompiler
    {
        private readonly CompilerArguments arguments;

        private readonly Logger logger;

        public SectorFileCompiler(CompilerArguments arguments, Logger logger)
        {
            this.arguments = arguments;
            this.logger = logger;
        }

        public void Compile()
        {
            logger.Info(this.arguments.ToString());
        }
    }
}
