using System.IO;
using Compiler.Event;
using Compiler.Input.Event;
using Compiler.Input.Rule;

namespace Compiler.Input.Validator
{
    public class FileExists: IFileValidator
    {
        public bool Validate(string path, IRuleDescriptor ruleDescriptor, IEventLogger eventLogger)
        {
            var exists = File.Exists(path);
            if (!exists)
            {
                eventLogger.AddEvent(new InputFileDoesNotExist(path, ruleDescriptor));
            }

            return exists;
        }
    }
}
