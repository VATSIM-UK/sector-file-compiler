using System;
using System.Collections.Generic;
using Compiler.Argument;

namespace CompilerCli.Input
{
    public class ArgumentParser
    {
        private static readonly Dictionary<string, IInputParser> availableArguments = new Dictionary<string, IInputParser>()
        {
            { "--config-file", new ConfigFileParser() },
            { "--out-file", new OutputFileParser() },
            { "--ignore-validation", new IgnoreValidationParser() },
            { "--strip-comments", new StripCommentsParser() },
            { "--strip-newlines", new StripNewlinesParser() },
            { "--test-arg", new TestArgumentParser() },
        };

        public static CompilerArguments CreateFromCommandLine(string[] args)
        {
            CompilerArguments arguments = new CompilerArguments();

            int i = 0;
            while (i < args.Length)
            {
                if (!availableArguments.ContainsKey(args[i]))
                {
                    throw new ArgumentException("Unknown argument: " + args[i]);
                }

                int nextArgument = i + 1;
                List<string> values = new List<string>();
                while (nextArgument < args.Length)
                {
                    if (args[nextArgument].StartsWith("--")) {
                        if (!availableArguments.ContainsKey(args[nextArgument]))
                        {
                            throw new ArgumentException("Unknown argument: " + args[i]);
                        }

                        break;
                    }

                    values.Add(args[nextArgument]);
                    nextArgument++;
                }

                arguments = availableArguments[args[i]].Parse(values, arguments);
                i = nextArgument;
            }

            return arguments;
        }
    }
}
