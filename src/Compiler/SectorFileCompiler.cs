using Compiler.Argument;
using Compiler.Input;
using Compiler.Output;
using System.Collections.Generic;
using Compiler.Model;
using Compiler.Event;
using Newtonsoft.Json;
using Compiler.Validate;
using Compiler.Parser;

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

            // Make the ESE
            foreach (OutputSections section in this.arguments.EseSections)
            {
                foreach (ICompilable compilable in sectorElements.Compilables[section])
                {
                    this.arguments.OutFileEse.Write(compilable.Compile());
                }
            }

            // Make the SCT2
            foreach (OutputSections section in this.arguments.SctSections)
            {
                foreach (ICompilable compilable in sectorElements.Compilables[section])
                {
                    this.arguments.OutFileSct.Write(compilable.Compile());
                }
            }

            this.events.AddEvent(new CompilationFinishedEvent(true));
            return 0;
        }
    }
}
