using Compiler.Argument;
using Compiler.Input;
using Compiler.Output;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

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

            dynamic test = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(
                this.arguments.ConfigFile.Contents()
            );

            // Make the ESE
            SectionFactory factory = new SectionFactory(new FileIndexer(this.arguments.ConfigFile.DirectoryLocation(), test, logger));

            foreach (OutputSections section in this.arguments.EseSections)
            {
                factory.Create(section).WriteToFile(this.arguments.OutFile);
            }

            logger.Info("Great success");
        }
    }
}
