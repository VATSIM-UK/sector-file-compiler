using Compiler.Argument;

namespace CompilerCli.Input
{
    interface InputParserInterface
    {
        public CompilerArguments Parse(string argument, CompilerArguments compilerSettings);
    }
}
