using System;
using Compiler.Event;

namespace Compiler.Error
{
    public class SyntaxError : ICompilerEvent
    {
        private readonly string problem;
        private readonly string fileName;
        private readonly int itemNumber;

        public SyntaxError(
            string problem,
            string fileName,
            int itemNumber 
        )
        {
            this.problem = problem;
            this.fileName = fileName;
            this.itemNumber = itemNumber;
        }

        public string GetMessage()
        {
            return String.Format(
                "Syntax Error: {0} in {1} at position {2}",
                this.problem,
                this.fileName,
                this.itemNumber
            );
        }

        public bool IsFatal()
        {
            return true;
        }
    }
}
