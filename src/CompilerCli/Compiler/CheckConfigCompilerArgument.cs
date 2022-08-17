using System;
using System.Collections.Generic;
using Compiler.Argument;

namespace CompilerCli.Compiler
{
    public class CheckConfigCompilerArgument : AbstractCompilerArgument
    {
        public override void Parse(List<string> values, CompilerArguments compilerSettings)
        {
            if (values.Count != 0)
            {
                throw new ArgumentException("Check config argument does not take any options");
            }

            compilerSettings.Mode = RunMode.CHECK_CONFIG;
        }

        public override string GetSpecifier()
        {
            return "--check-config";
        }
    }
}
