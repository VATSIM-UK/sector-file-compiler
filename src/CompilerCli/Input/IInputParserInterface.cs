using Compiler.Argument;

namespace CompilerCli.Input
{
    interface IInputParserInterface
    {
        public CompilerArguments Parse(string argument, CompilerArguments compilerSettings);
    }
}
