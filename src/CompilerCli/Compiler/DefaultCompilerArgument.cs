using System.Collections.Generic;
using Compiler.Argument;

namespace CompilerCli.Compiler
{
    public class DefaultCompilerArgument : AbstractCompilerArgument
    {
        public override void Parse(List<string> values, CompilerArguments compilerSettings)
        {
            // Do nothing, this is a test
        }

        public override string GetSpecifier()
        {
            return "--test-arg";
        }
    }
}
