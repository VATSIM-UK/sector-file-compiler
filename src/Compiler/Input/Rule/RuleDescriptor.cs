namespace Compiler.Input.Rule
{
    public class RuleDescriptor: IRuleDescriptor
    {
        private readonly string details;

        public RuleDescriptor(string details)
        {
            this.details = details;
        }

        public string GetDescriptor()
        {
            return details;
        }
    }
}
