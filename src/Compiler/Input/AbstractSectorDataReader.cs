using System.Collections.Generic;
using System.Linq;
using Compiler.Model;

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
        public Comment GetCommentSegment(string line)
        {
            int commentIndex = line.IndexOf(this.GetCommentDelimiter());
            return new Comment(commentIndex == -1 ? "" : line.Substring(commentIndex + 1).Trim());
        }

        /**
         * This method should return the character that is the data delimiting character.
         * In SCT format files this is usually a single space (with some exceptions), in ESE format
         * files this is a colon.
         */
        protected abstract string GetDataDelimiter();

        /**
         * This method should return the character that is used to signal the start of a comment.
         * In all current cases this is the semicolon, but this may change in the future.
         */
        protected abstract string GetCommentDelimiter();

        /**
         * Returns whether or not files should allow extra whitespaces. In the ESE extra whitespace is taken
         * as meaningful, whereas in the SCT it is ignored as just extra delimiters or trying to get data to align.
         */
        protected abstract bool FilterExtraWhitespace();

        /**
         * Returns a list of segments, that have been split up from the original line using the
         * data delimiter, once the comment has been stripped off.
         */
        public List<string> GetDataSegments(string line)
        {
            return new List<string>(this.GetRawData(line).Split(this.GetDataDelimiter()))
                .Where(segment => !this.FilterExtraWhitespace() || !string.IsNullOrEmpty(segment))
                .ToList();
        }

        /**
         * Returns the raw data for the line, that is, the unaltered line up until the comment character.
         */
        public string GetRawData(string line)
        {
            string preparedLine = this.PrepareLine(line);
            int commentIndex = preparedLine.IndexOf(this.GetCommentDelimiter());
            return commentIndex == -1
                ? preparedLine.Trim()
                : preparedLine.Substring(0, commentIndex).Trim();
        }

        private string PrepareLine(string line)
        {
            return line.Replace('\t', ' ');
        }
    }
}
