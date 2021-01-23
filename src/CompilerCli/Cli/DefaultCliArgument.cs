using System.Collections.Generic;

namespace CompilerCli.Cli
{
    public class DefaultCliArgument: AbstractCliArgument
    {
        public override void Parse(List<string> values, CliArguments cliArguments)
        {
            // Do nothing, this is a test
        }

        public override string GetSpecifier()
        {
            return "--test-cli-arg";
        }
    }
}