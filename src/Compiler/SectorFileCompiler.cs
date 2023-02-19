using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Compiler.Argument;
using Compiler.Collector;
using Compiler.Input;
using Compiler.Output;
using Compiler.Model;
using Compiler.Event;
using Compiler.Validate;
using Compiler.Parser;
using Compiler.Config;
using Compiler.Exception;
using Compiler.Injector;

namespace Compiler
{
    public class SectorFileCompiler
    {
        private readonly CompilerArguments arguments;
        private readonly EventTracker events;
        private const int SUCESS_RETURN = 0;
        private const int FAILURE_RETURN = 1;

        public SectorFileCompiler(CompilerArguments arguments, EventTracker events)
        {
            this.arguments = arguments;
            this.events = events;
        }

        /**
         * Run the compiler.
         */
        [ExcludeFromCodeCoverage]
        public int Compile()
        {
            events.AddEvent(new ComplilationStartedEvent());

            CompilerArgumentsValidator.Validate(events, arguments);
            if (events.HasFatalError())
            {
                events.AddEvent(new CompilationFinishedEvent(false));
                return FAILURE_RETURN;
            }

            // Parse all the config files
            OutputGroupRepository outputGroups = new OutputGroupRepository();

            ConfigInclusionRules config;
            try
            {
                events.AddEvent(new CompilationMessage("Loading config files"));
                config = ConfigFileLoaderFactory.Make(arguments).LoadConfigFiles(arguments.ConfigFiles, arguments);
            }
            catch (ConfigFileInvalidException e)
            {
                events.AddEvent(new CompilationMessage(e.Message));
                events.AddEvent(new CompilationFinishedEvent(false));
                return FAILURE_RETURN;
            }
            
            events.AddEvent(new CompilationMessage("Config files loaded successfully"));
            if (arguments.Mode == RunMode.CHECK_CONFIG)
            {
                return SUCESS_RETURN;
            }

            // Parse all the input files and create elements
            SectorElementCollection sectorElements = new SectorElementCollection();
            DataParserFactory parserFactory = new DataParserFactory(sectorElements, events);
            InputFileList fileList;
            try
            {
                events.AddEvent(new CompilationMessage("Building input file list"));
                fileList = InputFileListFactory.CreateFromInclusionRules(
                    new SectorDataFileFactory(new InputFileStreamFactory()),
                    config,
                    outputGroups,
                    events
                );
            }
            catch (System.Exception exception)
            {
                events.AddEvent(new CompilationMessage(exception.Message));
                events.AddEvent(new CompilationFinishedEvent(false));
                return FAILURE_RETURN;
            }

            if (events.HasFatalError())
            {
                events.AddEvent(new CompilationFinishedEvent(false));
                return FAILURE_RETURN;
            }

            events.AddEvent(new CompilationMessage("Injecting pre-parse static data"));
            RunwayCentrelineInjector.InjectRunwayCentrelineData(sectorElements);

            events.AddEvent(new CompilationMessage("Parsing input files"));
            foreach (AbstractSectorDataFile dataFile in fileList)
            {
                parserFactory.GetParserForFile(dataFile).ParseData(dataFile);
            }

            if (events.HasFatalError())
            {
                events.AddEvent(new CompilationFinishedEvent(false));
                return FAILURE_RETURN;
            }

            // There's some static data we need to inject to the collection for adjacent airports...
            events.AddEvent(new CompilationMessage("Injecting post-parse static data"));
            AdjacentAirportsInjector.InjectAdjacentAirportsData(sectorElements);

            // We aren't going beyond linting, so stop.
            if (arguments.Mode == RunMode.LINT)
            {
                return SUCESS_RETURN;
            }

            // Now all the data is loaded, validate that there are no broken references etc.
            if (arguments.ValidateOutput)
            {
                events.AddEvent(new CompilationMessage("Validating data"));
                OutputValidator.Validate(sectorElements, arguments, events);
                if (events.HasFatalError())
                {
                    events.AddEvent(new CompilationFinishedEvent(false));
                    return FAILURE_RETURN;
                }
            }
            else
            {
                events.AddEvent(new CompilationMessage("Skipping output validation"));
            }

            // We aren't going beyond post-validating, so don't compile.
            if (arguments.Mode == RunMode.VALIDATE)
            {
                return SUCESS_RETURN;
            }

            // Generate the output - all at once
            OutputGenerator generator = new OutputGenerator(
                sectorElements,
                outputGroups,
                new CompilableElementCollectorFactory(sectorElements, outputGroups),
                arguments
            );

            var outputTasks = new List<Task>();
            foreach (AbstractOutputFile output in arguments.OutputFiles)
            {
                events.AddEvent(new CompilationMessage($"Generating {output.GetFileDescriptor()} output"));
                outputTasks.Add(generator.GenerateOutput(output));
            }

            Task.WaitAll(outputTasks.ToArray());

            events.AddEvent(new CompilationFinishedEvent(true));
            return SUCESS_RETURN;
        }
    }
}
