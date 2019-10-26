using Compiler.Error;
using Compiler.Event;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Compiler.Argument
{
    public class CompilerArgumentsValidator
    {
        public static void Validate(IEventLogger events, CompilerArguments arguments)
        {
            if (!arguments.ConfigFile.Exists())
            {
                events.AddEvent(
                    new CompilerArgumentError("The configuration file could not be found: " + arguments.ConfigFile.GetPath())
                );
            }

            if (arguments.ConfigFile.Exists())
            {
                try
                {
                    JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(arguments.ConfigFile.Contents());
                }
                catch
                {
                    events.AddEvent(new CompilerArgumentError("The configuration file is not valid JSON"));
                }
            }
        }
    }
}
