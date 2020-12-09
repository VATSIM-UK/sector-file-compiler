using Compiler.Config;
using Compiler.Output;

namespace Compiler.Input
{
    public class InputFileListFactory
    {
        public static InputFileList CreateFromInclusionRules(ConfigInclusionRules config, OutputGroupRepository outputGroups)
        {
            InputFileList fileList = new InputFileList();

            foreach (IInclusionRule rule in config)
            {
                OutputGroup group = rule.GetOutputGroup();
                foreach (AbstractSectorDataFile file in rule.GetFilesToInclude())
                {
                    fileList.Add(file);
                    group.AddFile(file.FullPath);
                }

                outputGroups.Add(group);
            }
            return fileList;
        }
    }
}
