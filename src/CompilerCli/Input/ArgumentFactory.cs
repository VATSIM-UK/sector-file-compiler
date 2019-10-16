using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Input;

namespace CompilerCli.Input
{
    class ArgumentFactory
    {
        public static List<Argument> CreateFromCommandLine(string[] args)
        {
            Dictionary<string, ArgumentType> validArguments = new Dictionary<string, ArgumentType>() {
                { "--config-file", ArgumentType.ConfigFile }
            };

            List<Argument> arguments = new List<Argument>();
            if (args.Length < 2)
            {
                return arguments;
            }

            int i = 0;
            while (i < args.Length - 1)
            {
                if (!validArguments.ContainsKey(args[i]))
                {
                    throw new ArgumentException("Unknown argument: " + args[i]);
                }

                arguments.Add(new Argument(validArguments[args[i]], args[i + 1]));
                i = i + 2;
            }

            return arguments;
        }
    }
}
