using Compiler.Argument;
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

            // Parse all the config files
            OutputGroupRepository outputGroups = new OutputGroupRepository();

            ConfigInclusionRules config;
            try
            {
                config = new ConfigFileLoader().LoadConfigFiles(this.arguments.ConfigFiles);
            } catch (ConfigFileInvalidException e)
            {
                this.events.AddEvent(new CompilationMessage(e.Message));
                this.events.AddEvent(new CompilationFinishedEvent(false));
                return 1;
            }

            // Parse all the input files and create elements
            SectorElementCollection sectorElements = new SectorElementCollection();
            DataParserFactory parserFactory = new DataParserFactory(sectorElements, events);
            InputFileList fileList;
            try
            {
                fileList = InputFileListFactory.CreateFromInclusionRules(config, outputGroups);
            }
            catch (System.Exception exception)
            {
                this.events.AddEvent(new CompilationMessage(exception.Message));
                this.events.AddEvent(new CompilationFinishedEvent(false));
                return 1;
            }
            
            foreach (AbstractSectorDataFile dataFile in fileList)
            {
                parserFactory.GetParserForFile(dataFile).ParseData(dataFile);
            }

            if (this.events.HasFatalError())
            {
                this.events.AddEvent(new CompilationFinishedEvent(false));
                return 1;
            }
            
            // There's some static data we need to inject to the collection for adjacent airports...
            AdjacentAirportsInjector.InjectAdjacentAirportsData(sectorElements);
            

            // Now all the data is loaded, validate that there are no broken references etc.
            if (false)
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

            // Generate the output
            OutputGenerator generator = new OutputGenerator(
                sectorElements,
                outputGroups,
                new CompilableElementCollectorFactory(sectorElements, outputGroups)
            );
            foreach(AbstractOutputFile output in this.arguments.OutputFiles)
            {
                generator.GenerateOutput(output);
            }

            this.events.AddEvent(new CompilationFinishedEvent(true));
            return 0;
        }
    }
}
