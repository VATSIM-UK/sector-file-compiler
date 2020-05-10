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
using Compiler.Config;
using Compiler.Error;
using System;

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
            Dictionary<string, List<string>> mergedConfig;
            try
            {
                mergedConfig = ConfigFileMerger.MergeConfigFiles(this.arguments);
            } catch (Exception e)
            {
                this.events.AddEvent(new ConfigFileValidationError(e.Message));
                this.events.AddEvent(new CompilationFinishedEvent(false));
                return 1;
            }

            dynamic configFile = ConfigFileMerger.MergeConfigFiles(this.arguments);
            FileIndex files = FileIndexFactory.CreateFileIndex(configFile, events);

            // Parse all the input files
            SectorElementCollection sectorElements = new SectorElementCollection();
            SectorDataProcessor.Parse(
                new SectionParserFactory(sectorElements, events),
                sectorElements,
                this.arguments,
                files,
                events
            );

            if (this.events.HasFatalError())
            {
                this.events.AddEvent(new CompilationFinishedEvent(false));
                return 1;
            }

            // Validate the output files
            if (this.arguments.ValidateOutput)
            {
                OutputValidator.Validate(sectorElements, this.arguments, this.events);
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
            CompileEngineFactory.Create(arguments, sectorElements).Compile();

            this.events.AddEvent(new CompilationFinishedEvent(true));
            return 0;
        }
    }
}
