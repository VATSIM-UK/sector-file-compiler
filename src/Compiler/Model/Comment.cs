using System;

namespace Compiler.Model
{
    public class Comment : ICompilable
    {
        public Comment(string comment)
        {
            CommentString = comment;
        }

        public string CommentString { get; }

        public string Compile()
        {
            return String.Format(
                "; {0}\r\n",
                this.CommentString
            );
        }
    }
}
