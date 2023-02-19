using System;
using System.Collections.Generic;
using Compiler.Argument;

namespace CompilerCli.Compiler;

public class PrettyCompilerArgument : AbstractCompilerArgument
{
    public override void Parse(List<string> values, CompilerArguments compilerSettings)
    {
        if (values.Count != 0)
        {
            throw new ArgumentException("Pretty argument does not take any options");
        }

        compilerSettings.Pretty = Pretty.PRETTY;
    }

    public override string GetSpecifier()
    {
        return "--pretty";
    }
}
