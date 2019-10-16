using Compiler.Input;
using System.Collections.Generic;

namespace Compiler.Setting
{
    public class CompilerSettingsFactory
    {
        public static CompilerSettings CreateFromArgs(List<Argument> arguments)
        {
            CompilerSettings settings = new CompilerSettings();
            foreach (Argument argument in arguments)
            {
                switch (argument.type)
                {
                    case ArgumentType.ConfigFile:
                    {
                            settings.ConfigFilePath = argument.value;
                            break;
                    }
                }
            }

            return settings;
        }
    }
}
