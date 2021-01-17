using System.Collections.Generic;
using Compiler.Argument;

namespace CompilerCli.Input
{
    public class DefaultArgument : AbstractArgument
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
