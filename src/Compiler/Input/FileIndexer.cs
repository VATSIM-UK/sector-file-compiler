using System;
using System.Collections.Generic;
using Compiler.Output;
using System.Text;

namespace Compiler.Input
{
    public class FileIndexer
    {
        private readonly Dictionary<string, List<string>> configFile;

        private readonly ConfigFileSectionsMapper sectionMap = new ConfigFileSectionsMapper();

        private readonly ILoggerInterface logger;

        private readonly string configFileFolder;

        public FileIndexer(string configFileFolder, Dictionary<string, List<string>> configFile, ILoggerInterface logger)
        {
            this.configFileFolder = configFileFolder;
            this.configFile = configFile;
            this.logger = logger;
        }

        public List<IFileInterface> CreateFileListForSection(OutputSections section)
        {
            List<IFileInterface> files = new List<IFileInterface>();

            if (!this.sectionMap.sectionMap.ContainsKey(section))
            {
                this.logger.Warning("Compiler does not have section mapping for " + section.ToString());
                return files;
            }

            if (!this.configFile.ContainsKey(this.sectionMap.sectionMap[section]))
            {
                this.logger.Warning("Config file does not contain corresponding section for " + section.ToString());
                return files;
            }

            foreach (string filePath in this.configFile[this.sectionMap.sectionMap[section]])
            {
                files.Add(new InputFile(this.configFileFolder + "\\" + filePath));
            }

            return files;
        }
    }
}
