namespace Compiler.Model
{
    public class Colour :ICompilable
    {
        public Colour(string name, int value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name { get; }
        public int Value { get; }

        public string Compile()
        {
            return string.Format(
                "#define {0} {1}\r\n",
                this.Name,
                this.Value
            );
        }
    }
}
