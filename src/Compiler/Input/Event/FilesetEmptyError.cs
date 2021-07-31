using Compiler.Input.Rule;

namespace Compiler.Input.Event
{
    public class FilesetEmptyError: FilesetEmptyEvent
    {
        public FilesetEmptyError(IRuleDescriptor ruleDescriptor) : base(ruleDescriptor)
        {
        }

        public new bool IsFatal()
        {
            return true;
        }
    }
}
