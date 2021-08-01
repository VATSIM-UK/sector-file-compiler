#nullable enable
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

        public override bool Equals(object? obj)
        {
            return obj is RuleDescriptor descriptor &&
                   Equals(descriptor);
        }

        protected bool Equals(RuleDescriptor other)
        {
            return details == other.details;
        }

        public override int GetHashCode()
        {
            return details.GetHashCode();
        }
    }
}
