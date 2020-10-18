using System;
using Compiler.Event;
using Compiler.Input;
using Compiler.Model;

namespace Compiler.Error
{
    public class SyntaxError : ICompilerEvent
    {
        private readonly string problem;
        private readonly Definition definition;

        public SyntaxError(
            string problem,
            SectorData line
        ) {
            this.problem = problem;
            this.definition = line.definition;
        }

        public string GetMessage()
        {
            return string.Format(
                "Syntax Error: {0} in {1} at line {2}",
                this.problem,
                this.definition.Filename,
                this.definition.LineNumber
            );
        }

        public bool IsFatal()
        {
            return true;
        }
    }
}
