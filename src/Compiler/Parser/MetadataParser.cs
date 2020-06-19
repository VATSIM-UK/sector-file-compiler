using Compiler.Model;
using Compiler.Output;

namespace Compiler.Parser
{
    public class MetadataParser
    {
        private readonly SectorElementCollection sectorElements;
        private readonly OutputSections section;
        private readonly Subsections subsection;

        public MetadataParser(
            SectorElementCollection sectorElements,
            OutputSections section,
            Subsections subsection
        ) {
            this.sectorElements = sectorElements;
            this.section = section;
            this.subsection = subsection;
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
                this.section,
                this.subsection
            );
            return true;
        }

        public bool ParseBlankLine(string line)
        {
            if (line.Trim() != "")
            {
                return false;
            }

            this.sectorElements.Add(new BlankLine(), this.section, this.subsection);
            return true;
        }
    }
}
