using Compiler.Model;
using Compiler.Output;

namespace Compiler.Parser
{
    public class MetadataParser
    {
        private readonly SectorElementCollection sectorElements;
        private readonly OutputSections section;

        public MetadataParser(
            SectorElementCollection sectorElements,
            OutputSections section
        ) {
            this.sectorElements = sectorElements;
            this.section = section;
        }

        public bool ParseCommentLine(string line)
        {
            string trimmedLine = line.Trim();
            if (!trimmedLine.StartsWith(";"))
            {
                return false;
            }

            this.sectorElements.Add(
                new CommentLine(LineCommentParser.ParseComment(line)),
                this.section
            );
            return true;
        }

        public bool ParseBlankLine(string line)
        {
            if (line.Trim() != "")
            {
                return false;
            }

            this.sectorElements.Add(new BlankLine(), this.section);
            return true;
        }
    }
}
