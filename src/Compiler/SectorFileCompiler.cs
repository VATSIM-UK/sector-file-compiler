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

        private readonly CompilerArgumentsValidator validator;

        public SectorFileCompiler(CompilerArguments arguments, CompilerArgumentsValidator validator, Logger logger)
        {
            this.arguments = arguments;
            this.validator = validator;
            this.logger = logger;
        }

        /**
         * Run the compiler.
         */
        public void Compile()
        {
            if (!this.validator.Validate(arguments))
            {
                return;
            }

            logger.Info(this.arguments.ToString());
        }
    }
}
