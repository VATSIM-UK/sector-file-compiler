using System;

namespace Compiler.Parser
{
    /**
     * Parses comments from lines of input.
     */
    public class LineCommentParser
    {
        private const char COMMENT_DELIMETER = ';';

        /**
         * Parses the comment line. Ignores the comment delimeter itself as we
         * insert this again later on.
         */
        public static string ParseComment(string line)
        {
            Int32 commentPos = line.IndexOf(LineCommentParser.COMMENT_DELIMETER);
            return commentPos == -1 ? "" : line.Substring(commentPos + 1).Trim();
        }
    }
}
