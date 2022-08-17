using System;
using System.Collections.Generic;
using Compiler.Argument;

namespace CompilerCli.Compiler
{
    public class ValidateCompilerArgument : AbstractCompilerArgument
    {
        public override void Parse(List<string> values, CompilerArguments compilerSettings)
        {
            if (values.Count != 0)
            {
                throw new ArgumentException("Validate argument does not take any options");
            }

            compilerSettings.Mode = RunMode.VALIDATE;
        }

        public override string GetSpecifier()
        {
            return "--validate";
        }
    }
}
