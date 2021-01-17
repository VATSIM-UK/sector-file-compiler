using System;
using System.Collections.Generic;
using System.Linq;
using Compiler.Argument;

namespace CompilerCli.Input
{
    public class ArgumentParser
    {
        private readonly SortedSet<AbstractArgument> availableArguments;
        private readonly CompilerArguments compilerArguments;

        internal ArgumentParser(SortedSet<AbstractArgument> availableArguments, CompilerArguments compilerArguments)
        {
            this.availableArguments = availableArguments;
            this.compilerArguments = compilerArguments;
        }
        public CompilerArguments CreateFromCommandLine(string[] args)
        {
            int i = 0;
            while (i < args.Length)
            {
                if (!TryGetArgument(args[i], out AbstractArgument matchedArgument))
                {
                    throw new ArgumentException("Unknown argument: " + args[i]);
                }

                int nextArgument = i + 1;
                List<string> values = new List<string>();
                while (nextArgument < args.Length)
                {
                    if (TryGetArgument(args[nextArgument], out _)) {
                        break;
                    }
                    
                    values.Add(args[nextArgument]);
                    nextArgument++;
                }

                matchedArgument.Parse(values, compilerArguments);
                i = nextArgument;
            }

            return compilerArguments;
        }

        /**
         * Given an argument specifier, try and find that argument
         */
        private bool TryGetArgument(string inputArgument, out AbstractArgument argument)
        {
            try
            {
                argument = this.availableArguments.First(arg => arg.GetSpecifier() == inputArgument);
                return true;
            } catch (InvalidOperationException)
            {
                argument = null;
                return false;
            }
        }
    }
}
