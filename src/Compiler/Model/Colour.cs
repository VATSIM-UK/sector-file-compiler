namespace Compiler.Model
{
    public class Colour : AbstractSectorElement, ICompilable
    {
        public Colour(string name, int value, string comment) : base(comment)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name { get; }
        public int Value { get; }

        public string Compile()
        {
            return string.Format(
                "#define {0} {1}{2}\r\n",
                this.Name,
                this.Value,
                this.Comment
            );
        }
    }
}
