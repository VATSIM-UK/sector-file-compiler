using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Input
{
    abstract public class AbstractSectorDataReader
    {
        /*
         * Returns whether the line is a blank line
         */
        public bool IsBlankLine(string line)
        {
            return line.Trim() == "";
        }

        /*
         * Returns whether or not the line is a comment line
         */
        public bool IsCommentLine(string line)
        {
            return line.TrimStart().StartsWith(this.GetCommentDelimiter());
        }

        /*
         * Returns the comment part of a line of data
         */
        public string GetCommentSegment(string line)
        {
            int commentIndex = line.IndexOf(this.GetCommentDelimiter());
            return commentIndex == -1 ? "" : line.Substring(commentIndex);
        }

        /*
         * This method should return the character that is the data delimiting character.
         * In SCT format files this is usually a single space (with some exceptions), in ESE format
         * files this is a colon.
         */
        protected abstract string GetDataDelimiter();

        /*
         * This method should return the character that is used to signal the start of a comment.
         * In all current cases this is the semicolon, but this may change in the future.
         */
        protected abstract string GetCommentDelimiter();

        /*
         * Returns a list of segements, that have been split up from the original line using the
         * data delimiter, once the comment has been stripped off.
         */
        public List<string> GetDataSegments(string line)
        {
            int commentIndex = line.IndexOf(this.GetCommentDelimiter());
            return commentIndex == -1 
                ? new List<string>(line.Trim().Split(this.GetDataDelimiter()))
                :  new List<string>(line.Substring(0, commentIndex).Trim().Split(this.GetDataDelimiter()));
        }
    }
}
