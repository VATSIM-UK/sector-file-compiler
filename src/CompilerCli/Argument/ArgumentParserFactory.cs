using System.Collections.Generic;
using CompilerCli.Cli;
using CompilerCli.Compiler;

namespace CompilerCli.Argument
{
    public static class ArgumentParserFactory
    {
        public static ArgumentParser Make()
        {
            return new(
                new SortedSet<AbstractCompilerArgument>
                {
                    new DefaultCompilerArgument(),
                    new SkipValidationCompilerArgument(),
                    new StripCommentsCompilerArgument(),
                    new ConfigFileCompilerArgument(),
                    new BuildVersionCompilerArgument(),
                    new CheckConfigCompilerArgument(),
                    new LintCompilerArgument(),
                    new ValidateCompilerArgument(),
                },
                new SortedSet<AbstractCliArgument>
                {
                    new DefaultCliArgument(),
                    new NoWaitCliArgument()
                }
            );
        }
    }
}
