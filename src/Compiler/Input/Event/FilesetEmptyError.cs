using Compiler.Input.Rule;

namespace Compiler.Input.Event
{
    public class FilesetEmptyError: FilesetEmptyEvent
    {
        public FilesetEmptyError(IRuleDescriptor ruleDescriptor) : base(ruleDescriptor)
        {
        }

        public override string GetMessage()
        {
            return $"ERROR: {base.GetMessage()}";
        }
        
        public override bool IsFatal()
        {
            return true;
        }
    }
}
