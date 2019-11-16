using Compiler.Argument;
using Compiler.Input;
using Compiler.Output;
using System.Collections.Generic;
using Compiler.Model;
using Compiler.Event;
using Newtonsoft.Json;
using Compiler.Validate;
using Compiler.Parser;
using Compiler.Compile;

namespace Compiler
{
    public class SectorFileCompiler
    {
        private readonly CompilerArguments arguments;
        private readonly EventTracker events;

        public SectorFileCompiler(CompilerArguments arguments, EventTracker events)
        {
            this.arguments = arguments;
            this.events = events;
        }

        /**
         * Run the compiler.
         */
        public int Compile()
        {
            this.events.AddEvent(new ComplilationStartedEvent());

            CompilerArgumentsValidator.Validate(this.events, this.arguments);
            if (this.events.HasFatalError())
            {
                this.events.AddEvent(new CompilationFinishedEvent(false));
                return 1;
            }

            // Parse the config file and index all the files
            dynamic configFile = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(
                this.arguments.ConfigFile.Contents()
            );

            FileIndex files = FileIndexFactory.CreateFileIndex(this.arguments.ConfigFile.DirectoryLocation(), configFile, events);

            // Parse all the input files
            SectorElementCollection sectorElements = new SectorElementCollection();
            SectorDataProcessor.Parse(
                new SectionParserFactory(sectorElements, events),
                files,
                events
            );

            // Validate the output files
            if (this.arguments.ValidateOutput)
            {
                OutputValidator.Validate(sectorElements, this.events);
                if (this.events.HasFatalError())
                {
                    this.events.AddEvent(new CompilationFinishedEvent(false));
                    return 1;
                }
            } 
            else
            {
                this.events.AddEvent(new CompilationMessage("Skipping output validation"));
            }

            // Perform the compilation
            CompilerEngineFactory.Create(arguments, sectorElements).Compile();

            this.events.AddEvent(new CompilationFinishedEvent(true));
            return 0;
        }
    }
}
