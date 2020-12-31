using System;

namespace Compiler.Model
{
    /*
     * Represents where something was defined.
     */
    public class Definition
    {
        public Definition(string filename, int lineNumber)
        {
            Filename = filename;
            LineNumber = lineNumber;
        }

        public string Filename { get; }
        public int LineNumber { get; }

        public override bool Equals(object? obj)
        {
            return obj is Definition compareDefinition && this.Equals(compareDefinition);
        }

        protected bool Equals(Definition other)
        {
            return Filename == other.Filename && LineNumber == other.LineNumber;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Filename, LineNumber);
        }
    }
}
