using System.Collections.Generic;
using Compiler.Argument;

namespace CompilerCli.Compiler
{
    public class StripCommentsCompilerArgument : AbstractCompilerArgument
    {
        public override void Parse(List<string> values, CompilerArguments compilerSettings)
        {
            compilerSettings.StripComments = true;
        }

        public override string GetSpecifier()
        {
            return "--strip-comments";
        }
    }
}
