using System;
using System.Collections.Generic;
using Compiler.Argument;

namespace CompilerCli.Input
{
    public class ArgumentParser
    {
        private static readonly Dictionary<string, IInputParserInterface> availableArguments = new Dictionary<string, IInputParserInterface>()
        {
            { "--config-file", new ConfigFileParser() },
            { "--out-file", new OutputFileParser() },
            { "--verbosity", new VerbosityParser() },
        };

        public static CompilerArguments CreateFromCommandLine(string[] args)
        {
            CompilerArguments arguments = new CompilerArguments();

            if (args.Length < 2)
            {
                return arguments;
            }

            int i = 0;
            while (i < args.Length - 1)
            {
                if (!availableArguments.ContainsKey(args[i]))
                {
                    throw new ArgumentException("Unknown argument: " + args[i]);
                }

                arguments = availableArguments[args[i]].Parse(args[i + 1], arguments);
                i+= 2;
            }

            return arguments;
        }
    }
}
