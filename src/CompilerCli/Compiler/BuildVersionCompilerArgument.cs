﻿using System;
using System.Collections.Generic;
using Compiler.Argument;

namespace CompilerCli.Compiler
{
    public class BuildVersionCompilerArgument : AbstractCompilerArgument
    {
        public override void Parse(List<string>values, CompilerArguments compilerSettings)
        {
            if (values.Count != 1)
            {
                throw new ArgumentException("Build version should have only one argument");
            }
            
            compilerSettings.BuildVersion = values[0];
        }

        public override string GetSpecifier()
        {
            return "--build-version";
        }
    }
}
