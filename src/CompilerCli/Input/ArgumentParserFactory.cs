using System.Collections.Generic;
using Compiler.Argument;

namespace CompilerCli.Input
{
    public static class ArgumentParserFactory
    {
        public static ArgumentParser Make()
        {
            return new ArgumentParser(
                new SortedSet<AbstractArgument>
                {
                    new DefaultArgument(),
                    new SkipValidationArgument(),
                    new StripCommentsArgument(),
                    new ConfigFileArgument()
                },
                CompilerArgumentsFactory.Make()
            );
        }
    }
}