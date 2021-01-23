using System;
using System.Collections.Generic;

namespace CompilerCli.Cli
{
    public class NoWaitCliArgument: AbstractCliArgument
    {
        public override void Parse(List<string> values, CliArguments cliArguments)
        {
            if (values.Count != 0)
            {
                throw new ArgumentException("No wait flag cannot take any arguments");
            }

            cliArguments.PauseOnFinish = false;
        }

        public override string GetSpecifier()
        {
            return "--no-wait";
        }
    }
}