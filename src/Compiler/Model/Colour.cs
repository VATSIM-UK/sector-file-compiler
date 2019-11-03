namespace Compiler.Model
{
    public class Colour
    {
        public Colour(string name, int value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name { get; }
        public int Value { get; }
    }
}
