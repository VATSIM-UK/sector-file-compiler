using System;
using System.Data;
using Compiler.Argument;
using System.Collections.Generic;
using Compiler.Output;

namespace CompilerCli.Input
{
    public class VerbosityParser : IInputParserInterface
    {
        private static readonly Dictionary<string, OutputVerbosity> verbosityMap = new Dictionary<string, OutputVerbosity>()
        {
            { "debug", OutputVerbosity.Debug },
            { "info", OutputVerbosity.Info },
            { "warning", OutputVerbosity.Warning },
            { "error", OutputVerbosity.Error },
            { "quiet", OutputVerbosity.Null },
        };

        public CompilerArguments Parse(string argument, CompilerArguments compilerSettings)
        {
            if (!verbosityMap.ContainsKey(argument))
            {
                throw new ArgumentException("Invalid verbosity option");
            }

            compilerSettings.Verbosity = verbosityMap[argument];

            return compilerSettings;
        }
    }
}
