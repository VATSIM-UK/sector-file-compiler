using System;
using System.Collections.Generic;
using Compiler.Argument;

namespace CompilerCli.Compiler
{
    public class LintCompilerArgument : AbstractCompilerArgument
    {
        public override void Parse(List<string> values, CompilerArguments compilerSettings)
        {
            if (values.Count != 0)
            {
                throw new ArgumentException("Lint argument does not take any options");
            }

            compilerSettings.Mode = RunMode.LINT;
        }

        public override string GetSpecifier()
        {
            return "--lint";
        }
    }
}
