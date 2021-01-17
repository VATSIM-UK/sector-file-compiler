using System;
using System.Collections.Generic;
using Compiler.Argument;

namespace CompilerCli.Input
{
    public class ConfigFileArgument : AbstractArgument
    {
        public override void Parse(List<string>values, CompilerArguments compilerSettings)
        {
            if (values.Count != 1)
            {
                throw new ArgumentException("Config file path should have only one argument");
            }

            compilerSettings.ConfigFiles.Add(values[0]);
        }

        public override string GetSpecifier()
        {
            return "--config-file";
        }
    }
}
