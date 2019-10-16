namespace Compiler.Input
{
    public struct Argument
    {
        public Argument(ArgumentType type, dynamic value)
        {
            this.type = type;
            this.value = value;
        }

        // The type of argument
        public readonly ArgumentType type;

        // The argument value
        public readonly dynamic value;
    }
}
