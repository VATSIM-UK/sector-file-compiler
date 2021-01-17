using System;
using System.Collections.Generic;
using Compiler.Argument;
using Compiler.Model;

namespace CompilerCli.Input
{
    public class BuildVersionArgument : AbstractArgument
    {
        public override void Parse(List<string>values, CompilerArguments compilerSettings)
        {
            if (values.Count != 1)
            {
                throw new ArgumentException("Build version should have only one argument");
            }
            
            compilerSettings.ConfigFiles.Add(values[0]);
        }

        public override string GetSpecifier()
        {
            return "--build-version";
        }
    }
}
