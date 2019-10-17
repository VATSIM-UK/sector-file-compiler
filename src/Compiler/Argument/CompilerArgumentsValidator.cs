using Compiler.Output;
using System.Collections.Generic;

namespace Compiler.Argument
{
    public class CompilerArgumentsValidator
    {
        private readonly ILoggerInterface logger;
        public CompilerArgumentsValidator(ILoggerInterface logger)
        {
            this.logger = logger;
        }

        public bool Validate(CompilerArguments arguments)
        {
            bool valid = true;
            if (!arguments.ConfigFile.Exists())
            {
                valid = false;
                this.logger.Error("The configuration file could not be found: " + arguments.ConfigFile.GetPath());
            }

            return valid;
        }
    }
}
