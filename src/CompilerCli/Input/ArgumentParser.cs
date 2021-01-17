using System;
using System.Collections.Generic;
using Compiler.Argument;

namespace CompilerCli.Input
{
    public static class ArgumentParser
    {
        private static readonly Dictionary<string, IInputParser> AvailableArguments = new()
        {
            { "--config-file", new ConfigFileParser() },
            { "--skip-validation", new SkipValidationParser() },
            { "--strip-comments", new StripCommentsParser() },
            { "--test-arg", new DefaultArgumentParser() },
        };

        public static CompilerArguments CreateFromCommandLine(string[] args)
        {
            CompilerArguments arguments = new CompilerArguments();

            int i = 0;
            while (i < args.Length)
            {
                if (!AvailableArguments.ContainsKey(args[i]))
                {
                    throw new ArgumentException("Unknown argument: " + args[i]);
                }

                int nextArgument = i + 1;
                List<string> values = new List<string>();
                while (nextArgument < args.Length)
                {
                    if (args[nextArgument].StartsWith("--")) {
                        if (!AvailableArguments.ContainsKey(args[nextArgument]))
                        {
                            throw new ArgumentException("Unknown argument: " + args[i]);
                        }

                        break;
                    }

                    values.Add(args[nextArgument]);
                    nextArgument++;
                }

                arguments = AvailableArguments[args[i]].Parse(values, arguments);
                i = nextArgument;
            }

            return arguments;
        }
    }
}
