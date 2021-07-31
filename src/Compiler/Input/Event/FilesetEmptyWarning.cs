using Compiler.Input.Rule;

namespace Compiler.Input.Event
{
    public class FilesetEmptyWarning: FilesetEmptyEvent
    {
        public FilesetEmptyWarning(IRuleDescriptor ruleDescriptor) : base(ruleDescriptor)
        {
        }
    }
}
