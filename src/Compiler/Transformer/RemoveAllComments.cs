using System.Collections.Generic;

namespace Compiler.Transformer
{
    public class RemoveAllComments : ITransformer
    {
        const char COMMENT_DELIMTER = ';';

        public List<string> Transform(List<string> lines)
        {
            List<string> linesToKeep = new List<string>();
            foreach (string line in lines)
            {
                if (!line.Contains(RemoveAllComments.COMMENT_DELIMTER))
                {
                    linesToKeep.Add(line);
                    continue;
                }

                int commentPos = line.IndexOf(RemoveAllComments.COMMENT_DELIMTER);
                string lineWithoutComment = line.Substring(0, commentPos);

                if (lineWithoutComment.Trim() != "")
                {
                    linesToKeep.Add(lineWithoutComment.Trim());
                    continue;
                }
            }

            return linesToKeep;
        }
    }
}
