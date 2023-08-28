using System;
using System.Collections.Generic;

namespace CompilerCli.Cli
{
    public class HelpArgument : AbstractCliArgument
    {
        public override void Parse(List<string> values, CliArguments cliArguments) {
            if (values.Count != 0) {
                throw new ArgumentException("Help flag cannot take any arguments");
            }

            cliArguments.HelpExit = true;
        }

        public override string GetSpecifier() {
            return "--help";
        }

        public static string GetHelpMessage() {
            return @"
usage: CompilerCli.exe [options] --config-file [config-file]
    Compile the VATSIM-UK Sector File with the specified config file.
    For more information visit: https://github.com/VATSIM-UK/sector-file-compiler

    options:
        --config-file       Required. Takes a single argument. Path to a 
                            compiler configuration JSON file. If this argument 
                            is specified multiple times, then the compiler will
                            attempt to merge the configs together.
        --check-config      If set, only runs the configuration checking step 
                            to ensure that the compiler config is correct.
        --lint              If set, only runs the configuration check and 
                            linting steps.
        --validate          If set, only runs the configuration check, linting 
                            and post-validation steps. Does not output files.
        --skip-validation   If set, the compiler will skip the post-parse 
                            validation phase of compilation. If running in full
                            compilation mode, will still produce output.
        --pretty            If set, ""large"" elements such as SECTOR will have
                            an extra newline inserted between them, in order to
                            provide improved readability.
        --strip-comments    If set, any comments in the input will be removed. 
                            If an empty line is leftover, it will be discarded.
        --build-version     Takes a single argument. Specifies the version of 
                            the build to replace the {VERSION} token in the 
                            input.
        --no-wait           Prompts the compiler not to wait for a keypress 
                            when compilation has finished.
        --help              Display this help page.
";
        }
    }
}
