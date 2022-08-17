using System;
using System.Collections.Generic;
using System.Linq;
using Compiler.Argument;
using CompilerCli.Cli;
using CompilerCli.Compiler;

namespace CompilerCli.Argument
{
    public class ArgumentParser
    {
        private SortedSet<AbstractCompilerArgument> availableCompilerArguments;
        private SortedSet<AbstractCliArgument> availableCliArguments;

        internal ArgumentParser(
            SortedSet<AbstractCompilerArgument> availableCompilerArguments,
            SortedSet<AbstractCliArgument> availableCliArguments
        )
        {
            this.availableCompilerArguments = availableCompilerArguments;
            this.availableCliArguments = availableCliArguments;
        }
        public void CreateFromCommandLine(
            CompilerArguments compilerArguments,
            CliArguments cliArguments,
            string[] args
        ) {
            int i = 0;
            while (i < args.Length)
            {
                if (TryGetCompilerArgument(args[i], out AbstractCompilerArgument matchedCompilerArgument))
                {
                    i = ProcessArguments(
                        args,
                        i + 1,
                        arguments => matchedCompilerArgument.Parse(arguments, compilerArguments)
                    );
                } 
                else if (TryGetCliArgument(args[i], out AbstractCliArgument matchedCliArgument))
                {
                    i = ProcessArguments(
                        args,
                        i + 1,
                        arguments => matchedCliArgument.Parse(arguments, cliArguments)
                    );
                }
                else
                {
                    throw new ArgumentException("Unknown argument: " + args[i]);
                }
            }
        }

        public bool HasCompilerArgument(Type type)
        {
            return availableCompilerArguments.Any(arg => arg.GetType() == type);
        }
        
        public bool HasCliArgument(Type type)
        {
            return availableCliArguments.Any(arg => arg.GetType() == type);
        }

        private int ProcessArguments(string[] args, int startIndex, Action<List<string>> process)
        {
            int index = startIndex;
            List<string> values = new List<string>();
            while (index < args.Length)
            {
                if (IsNewArgument(args[index])) {
                    break;
                }
                    
                values.Add(args[index]);
                index++;
            }

            process(values);
            return index;
        }

        private bool IsNewArgument(string inputArgument)
        {
            return TryGetCompilerArgument(inputArgument, out _) || TryGetCliArgument(inputArgument, out _);
        }

        /**
         * Given an argument specifier for a compiler argument, try and find that argument
         */
        private bool TryGetCompilerArgument(string inputArgument, out AbstractCompilerArgument compilerArgument)
        {
            try
            {
                compilerArgument = availableCompilerArguments.First(arg => arg.GetSpecifier() == inputArgument);
                return true;
            } catch (InvalidOperationException)
            {
                compilerArgument = null;
                return false;
            }
        }
        
        /**
         * Given an argument specifier for a CLI argument, try and find that argument
         */
        private bool TryGetCliArgument(string inputArgument, out AbstractCliArgument cliArgument)
        {
            try
            {
                cliArgument = availableCliArguments.First(arg => arg.GetSpecifier() == inputArgument);
                return true;
            } catch (InvalidOperationException)
            {
                cliArgument = null;
                return false;
            }
        }
    }
}
