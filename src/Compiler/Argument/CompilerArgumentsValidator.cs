using Compiler.Error;
using Compiler.Event;
using System.Collections.Generic;
using Newtonsoft.Json;
using Compiler.Input;

namespace Compiler.Argument
{
    public class CompilerArgumentsValidator
    {
        public static void Validate(IEventLogger events, CompilerArguments arguments)
        {
            if (arguments.ConfigFiles.Count == 0)
            {
                events.AddEvent(
                    new CompilerArgumentError("No config files specificed")
                );
            }
        }
    }
}
