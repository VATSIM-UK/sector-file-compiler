using Compiler.Argument;
using Compiler.Input;
using Compiler.Output;
using System.Collections.Generic;
using Compiler.Model;
using Compiler.Event;
using Newtonsoft.Json;
using Compiler.Validate;

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
            SectorElementCollection sectorElements = SectorElementCollectionFactory.Create(
                new Parser.SectionParserFactory(events),
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

            // Make the ESE
            SectionFactory factory = new SectionFactory(files);
            foreach (OutputSections section in this.arguments.EseSections)
            {
                factory.Create(section).WriteToFile(this.arguments.OutFile);
            }

            // Make the SCT2
            //TODO

            this.events.AddEvent(new CompilationFinishedEvent(true));
            return 0;
        }
    }
}
