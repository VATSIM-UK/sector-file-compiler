using System;

namespace Compiler.Model
{
    public class CommentLine : ICompilable
    {
        private readonly string comment;

        public CommentLine(string comment)
        {
            this.comment = comment;
        }

        public string Compile()
        {
            return String.Format(
                "; {0}\r\n",
                this.comment
            );
        }
    }
}
