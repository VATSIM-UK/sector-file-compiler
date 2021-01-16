using System;

namespace Compiler.Parser
{
    /**
     * Parses comments from lines of input.
     */
    public class LineCommentParser
    {
        private const char CommentDelimeter = ';';

        /**
         * Parses the comment line. Ignores the comment delimeter itself as we
         * insert this again later on.
         */
        public static string ParseComment(string line)
        {
            Int32 commentPos = line.IndexOf(LineCommentParser.CommentDelimeter);
            return commentPos == -1 ? null : line.Substring(commentPos + 1).Trim();
        }

        /**
        * Parses the comment line. Ignores the comment delimeter itself as we
        * insert this again later on.
        */
        public static string ParseData(string line)
        {
            Int32 commentPos = line.IndexOf(LineCommentParser.CommentDelimeter);
            return commentPos == -1 ? line.TrimEnd() : line.Substring(0, commentPos).TrimEnd();
        }
    }
}
