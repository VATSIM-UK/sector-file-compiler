using System;

namespace Compiler.Event
{
    /**
     * An event for when the parser wants to suggest a change or
     * hint at what may be causing parsing to fail.
     */
    public class ParserSuggestion : ICompilerEvent
    {
        private readonly string message;

        public ParserSuggestion(string message)
        {
            this.message = message;
        }

        public string GetMessage()
        {
            return $"SUGGESTION: {this.message}";
        }

        public bool IsFatal()
        {
            return false;
        }
    }
}
